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
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace MasterDetailInside

    Public Class ViewModel ' : INotifyPropertyChanged

        Private dataField As List(Of ParentTestData)

        Public ReadOnly Property Data As List(Of ParentTestData)
            Get
                If dataField Is Nothing Then
                    dataField = New List(Of ParentTestData)()
                    For i As Integer = 0 To 50 - 1
                        Dim parentTestData As ParentTestData = New ParentTestData() With {.Text = "Master" & i, .Number = i, .Data = New List(Of TestData)()}
                        For j As Integer = 0 To 10 - 1
                            Dim testData As TestData = New TestData() With {.Text = "Detail" & j & " Master" & i, .Number = j, .Ready = j Mod 2 <> 0, .Data = New List(Of DetailTestData)()}
                            For k As Integer = 0 To 5 - 1
                                testData.Data.Add(New DetailTestData() With {.Text = "NestedDetail" & k & " Master" & j, .Number = k, .Ready = j Mod 2 <> 0})
                            Next

                            parentTestData.Data.Add(testData)
                        Next

                        dataField.Add(parentTestData)
                    Next
                End If

                Return dataField
            End Get
        End Property
    End Class

    Public Class DetailTestData
        Implements IText

        Public Property Ready As Boolean

        Public Property Text As String Implements IText.Text

        Public Property Number As Integer
    End Class

    Public Class TestData
        Implements IText

        Public Property Ready As Boolean

        Public Property Text As String Implements IText.Text

        Public Property Number As Integer

        Public Property Data As List(Of DetailTestData)
    End Class

    Public Class ParentTestData
        Implements IText

        Public Property Text As String Implements IText.Text

        Public Property Number As Integer

        Public Property Data As List(Of TestData)
    End Class

    Friend Interface IText

        Property Text As String

    End Interface
End Namespace
