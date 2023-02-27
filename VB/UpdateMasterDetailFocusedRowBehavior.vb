' Developer Express Code Central Example:
' Binding Master and Detail focused rows to ViewModel objects
' 
' This example demonstrates how to use ViewModel properties to track and control
' focused row changes both for Master and Detail grids. This capability is
' achieved by creating attached behavior that handles all necessary events
' (especially for the focused view and row changing, both in the detail and master
' grids). The logic of focusing rows can be changed inside this behavior depending
' on your requirements. For example, you may want not to focus the first detail
' row when a master row is expanded. If so, change the MasterRowExpanded event
' handler as follows:
' 
' void MasterGridMasterRowExpanded(object sender,
' RowEventArgs e)
' {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
' GridControl).View.FocusedRow = null;
' }
' 
' 
' 
' 
' So, the main idea of the example is
' to show how the row focusing logic can be defined in the GridControl that
' operates in Master-Detail mode.
' 
' Important note:the approach shown in the
' example will work only with the DataControlDetailDescriptor, as other
' descriptors use custom templates to display detail content that is not
' synchronized with the master grid.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4402
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Mvvm.UI.Interactivity
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace MasterDetailInside

    Public Class UpdateMasterDetailFocusedRowBehavior
        Inherits Behavior(Of TableView)

        Public Shared ReadOnly CurrentItemProperty As DependencyProperty = DependencyProperty.RegisterAttached("CurrentItem", GetType(Object), GetType(UpdateMasterDetailFocusedRowBehavior), New FrameworkPropertyMetadata(AddressOf CurrentItemPropertyChanged))

        Public Shared Sub SetCurrentItem(ByVal element As UIElement, ByVal value As Object)
            element.SetValue(CurrentItemProperty, value)
        End Sub

        Public Shared Function GetCurrentItem(ByVal element As UIElement) As Object
            Return CObj(element.GetValue(CurrentItemProperty))
        End Function

        Public Shared ReadOnly MasterGridBehaviorProperty As DependencyProperty = DependencyProperty.RegisterAttached("MasterGridBehavior", GetType(UpdateMasterDetailFocusedRowBehavior), GetType(UpdateMasterDetailFocusedRowBehavior), Nothing)

        Public Shared Sub SetMasterGridBehavior(ByVal element As UIElement, ByVal value As UpdateMasterDetailFocusedRowBehavior)
            element.SetValue(MasterGridBehaviorProperty, value)
        End Sub

        Public Shared Function GetMasterGridBehavior(ByVal element As UIElement) As UpdateMasterDetailFocusedRowBehavior
            Return CType(element.GetValue(MasterGridBehaviorProperty), UpdateMasterDetailFocusedRowBehavior)
        End Function

        Public Shared ReadOnly GridNestingLevelProperty As DependencyProperty = DependencyProperty.RegisterAttached("GridNestingLevel", GetType(Integer), GetType(UpdateMasterDetailFocusedRowBehavior), Nothing)

        Public Shared Sub SetGridNestingLevel(ByVal element As UIElement, ByVal value As Integer)
            element.SetValue(GridNestingLevelProperty, value)
        End Sub

        Public Shared Function GetGridNestingLevel(ByVal element As UIElement) As Integer
            Return CInt(element.GetValue(GridNestingLevelProperty))
        End Function

        Private levelValue As Dictionary(Of Integer, Object) = New Dictionary(Of Integer, Object)()

        Private boundCurrentItemLockCount As Integer = 0

        Private gridCurrentItemLockCount As Integer = 0

        Private maxNestingLevel As Integer = 0

        Protected Overrides Sub OnAttached()
            maxNestingLevel = GetMaxNestingLevel()
            InitGridEventsAndProperties(CType(AssociatedObject.DataControl, GridControl), 0)
            AddHandler AssociatedObject.FocusedViewChanged, AddressOf View_FocusedViewChanged
        End Sub

        Private Function GetMaxNestingLevel() As Integer
            Dim nestingLevel As Integer = 0
            Dim tempGrid As GridControl = CType(AssociatedObject.DataControl, GridControl)
            While True
                Dim detailDescriptor As DataControlDetailDescriptor = TryCast(tempGrid.DetailDescriptor, DataControlDetailDescriptor)
                If detailDescriptor IsNot Nothing Then
                    tempGrid = CType(detailDescriptor.DataControl, GridControl)
                    nestingLevel += 1
                Else
                    Exit While
                End If
            End While

            Return nestingLevel
        End Function

        Private Shared Sub CurrentItemPropertyChanged(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim grid As GridControl = TryCast(source, GridControl)
            Dim behavior As UpdateMasterDetailFocusedRowBehavior = GetMasterGridBehavior(grid)
            If behavior Is Nothing Then Return
            If behavior.boundCurrentItemLockCount = 0 Then
                behavior.BoundCurrentItemChanged(grid, e.NewValue)
            End If
        End Sub

        Private Sub BoundCurrentItemChanged(ByVal virtualGrid As GridControl, ByVal newCurrentItem As Object)
            Dim GridNestingLevel As Integer = GetGridNestingLevel(virtualGrid)
            levelValue(GridNestingLevel) = newCurrentItem
            Dim visualGrid As GridControl = CType(AssociatedObject.DataControl, GridControl)
            Dim tempGrid As GridControl
            gridCurrentItemLockCount += 1
            For i As Integer = 0 To GridNestingLevel - 1
                Dim rowHandle As Integer = visualGrid.DataController.FindRowByRowValue(levelValue(i))
                tempGrid = CType(visualGrid.GetDetail(rowHandle), GridControl)
                If tempGrid Is Nothing Then
                    If newCurrentItem Is Nothing Then Return
                    visualGrid.ExpandMasterRow(rowHandle)
                    tempGrid = CType(visualGrid.GetDetail(rowHandle), GridControl)
                    If tempGrid Is Nothing Then Return
                End If

                visualGrid = tempGrid
            Next

            Dim newFocusedRowHandle As Integer = visualGrid.DataController.FindRowByRowValue(newCurrentItem)
            visualGrid.View.MoveFocusedRow(newFocusedRowHandle)
            gridCurrentItemLockCount -= 1
        End Sub

        Private Sub InitGridEventsAndProperties(ByVal grid As GridControl, ByVal level As Integer)
            AddHandler grid.CurrentItemChanged, AddressOf grid_CurrentItemChanged
            grid.SetValue(MasterGridBehaviorProperty, Me)
            grid.SetValue(GridNestingLevelProperty, level)
            Dim detailDescriptor As DataControlDetailDescriptor = TryCast(grid.DetailDescriptor, DataControlDetailDescriptor)
            If detailDescriptor IsNot Nothing Then InitGridEventsAndProperties(CType(detailDescriptor.DataControl, GridControl), level + 1)
        End Sub

        Private Sub View_FocusedViewChanged(ByVal sender As Object, ByVal e As FocusedViewChangedEventArgs)
            If gridCurrentItemLockCount = 0 Then InvalidateCurrentRow()
        End Sub

        Private Sub grid_CurrentItemChanged(ByVal sender As Object, ByVal e As CurrentItemChangedEventArgs)
            If gridCurrentItemLockCount = 0 Then
                Dim gridLevel As Integer = GetGridNestingLevel(CType(sender, GridControl))
                For i As Integer = gridLevel + 1 To maxNestingLevel
                    levelValue(i) = Nothing
                Next

                InvalidateCurrentRow()
            End If
        End Sub

        Private Sub InvalidateCurrentRow()
            Dim focusedGrid As GridControl = CType(AssociatedObject.FocusedView.DataControl, GridControl)
            UpdateCurrentItems(focusedGrid, GetVisualGridNestingLevel(focusedGrid, 0), focusedGrid.CurrentItem)
            UpdateCurrentItemAttachedPropertyValue(CType(AssociatedObject.DataControl, GridControl), 0)
        End Sub

        Private Sub UpdateCurrentItems(ByVal grid As GridControl, ByVal level As Integer, ByVal levelCurrentItem As Object)
            Dim toModify = levelValue.Where(Function(i) i.Key > level).ToList()
            For Each item As KeyValuePair(Of Integer, Object) In toModify
                levelValue(item.Key) = Nothing
            Next

            UpdateCurrentItem(grid, level, levelCurrentItem)
        End Sub

        Private Sub UpdateCurrentItem(ByVal grid As GridControl, ByVal level As Integer, ByVal levelCurrentItem As Object)
            levelValue(level) = levelCurrentItem
            Dim masterGrid As GridControl = grid.GetMasterGrid()
            If masterGrid IsNot Nothing Then UpdateCurrentItem(masterGrid, level - 1, masterGrid.GetRow(grid.GetMasterRowHandle()))
        End Sub

        Private Sub UpdateCurrentItemAttachedPropertyValue(ByVal grid As GridControl, ByVal level As Integer)
            Dim currentItemValue As Object
            If Not levelValue.TryGetValue(level, currentItemValue) Then Return
            boundCurrentItemLockCount += 1
            grid.SetCurrentValue(CurrentItemProperty, currentItemValue)
            boundCurrentItemLockCount -= 1
            Dim detailDescriptor As DataControlDetailDescriptor = TryCast(grid.DetailDescriptor, DataControlDetailDescriptor)
            If detailDescriptor IsNot Nothing Then UpdateCurrentItemAttachedPropertyValue(CType(detailDescriptor.DataControl, GridControl), level + 1)
        End Sub

        Private Function GetVisualGridNestingLevel(ByVal grid As GridControl, ByVal startLevel As Integer) As Integer
            Dim masterGrid As GridControl = grid.GetMasterGrid()
            If masterGrid IsNot Nothing Then Return GetVisualGridNestingLevel(masterGrid, startLevel + 1)
            Return startLevel
        End Function
    End Class
End Namespace
