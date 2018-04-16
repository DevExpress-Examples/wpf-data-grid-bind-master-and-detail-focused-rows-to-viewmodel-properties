' Developer Express Code Central Example:
' GridControl - How to bind Master and Detail focused rows to ViewModel objects to follow the MVVM pattern in your application
' 
' This example demonstrates how to use ViewModel properties to track and control
' focused row changes both for Master and Detail grids. This capability is
' achieved by creating attached behavior that handles all necessary events
' (especially for the focused view and row changing, both in the detail and master
' grids). The logic of focusing rows can be changed inside this behavior depending
' on your requirements. For example, you may want not to focus the first detail
' row when a master row is expanded. If so, change the MasterRowExpanded event
' handler as follows:
' void MasterGridMasterRowExpanded(object sender,
' RowEventArgs e)
' {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
' GridControl).View.FocusedRow = null;
' }
' So, the main idea of the example is to
' show how the row focusing logic can be defined in the GridControl that operates
' in Master-Detail mode.
' Note that the approach shown in the example will work
' only with the DataControlDetailDescriptor, as other descriptors use custom
' templates to display detail content that is not synchronized with the master
' grid.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4402


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Grid
Imports System.Windows.Interactivity

Namespace E4402
	Public Class MasterFocusedRowBehavior
		Inherits Behavior(Of TableView)
        Public Shared ReadOnly FocusedRowProperty As DependencyProperty = DependencyProperty.Register("FocusedRow", GetType(Object), GetType(MasterFocusedRowBehavior), New UIPropertyMetadata(Nothing, AddressOf OnFocusedRowChanged))

		Private Shared Sub OnFocusedRowChanged(ByVal o As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim masterFocusedRowBehavior As MasterFocusedRowBehavior = TryCast(o, MasterFocusedRowBehavior)
			If masterFocusedRowBehavior IsNot Nothing Then
				masterFocusedRowBehavior.OnFocusedRowChanged(CObj(e.OldValue), CObj(e.NewValue))
			End If
		End Sub

		Protected Overridable Sub OnFocusedRowChanged(ByVal oldValue As Object, ByVal newValue As Object)
			If isChangeInternal Then
				isChangeInternal = False
				Return
			End If
			AssociatedObject.MoveFocusedRow((TryCast(AssociatedObject.DataControl, GridControl)).DataController.FindRowByRowValue(newValue))
		End Sub

		Public Property FocusedRow() As Object
			Get
				Return CObj(GetValue(FocusedRowProperty))
			End Get
			Set(ByVal value As Object)
				SetValue(FocusedRowProperty, value)
			End Set
		End Property

		Private Property FocusedRowInternal() As Object
			Get
				Return FocusedRow
			End Get
			Set(ByVal value As Object)
				If FocusedRow Is value Then
					Return
				End If
				isChangeInternal = True
				FocusedRow = value
			End Set
		End Property

		Private isChangeInternal As Boolean = False

		Protected Overrides Sub OnAttached()
			MyBase.OnAttached()
			AddHandler AssociatedObject.FocusedRowChanged, AddressOf AssociatedObject_FocusedRowChanged
			AddHandler AssociatedObject.FocusedViewChanged, AddressOf AssociatedObject_FocusedViewChanged
		End Sub

		Private Sub AssociatedObject_FocusedViewChanged(ByVal sender As Object, ByVal e As FocusedViewChangedEventArgs)
			AssociatedObject.FocusedRowHandle = (TryCast(e.NewView.DataControl, GridControl)).GetMasterRowHandle()
		End Sub

		Private Sub AssociatedObject_FocusedRowChanged(ByVal sender As Object, ByVal e As FocusedRowChangedEventArgs)
			FocusedRowInternal = e.NewRow
		End Sub

		Protected Overrides Sub OnDetaching()
			RemoveHandler AssociatedObject.FocusedRowChanged, AddressOf AssociatedObject_FocusedRowChanged
			MyBase.OnDetaching()
		End Sub
	End Class
End Namespace
