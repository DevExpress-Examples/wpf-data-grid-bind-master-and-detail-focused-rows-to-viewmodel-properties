Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm

Namespace MasterDetailInside
    Public Class ViewModel
        Inherits BindableBase

        Public Property Level1CurrentItem() As Item
            Get
                Return GetValue(Of Item)()
            End Get
            Set(value As Item)
                SetValue(value)
            End Set
        End Property

        Public Property Level2CurrentItem() As Item
            Get
                Return GetValue(Of Item)()
            End Get
            Set(value As Item)
                SetValue(value)
            End Set
        End Property

        Public Property Level3CurrentItem() As Item
            Get
                Return GetValue(Of Item)()
            End Get
            Set(value As Item)
                SetValue(value)
            End Set
        End Property

        Public ReadOnly Property Data() As ObservableCollection(Of Item)

        Public Sub New()
            Data = New ObservableCollection(Of Item)
            For i As Integer = 0 To 49
                Dim item1 As New Item() With {.Id = i, .Name = String.Format("Item_{0}", i)}
                For j As Integer = 0 To 9
                    Dim item2 As New Item() With {.Id = j, .Name = String.Format("Item_{0}.{1}", i, j)}
                    For k As Integer = 0 To 4
                        item2.Items.Add(New Item() With {.Id = k, .Name = String.Format("Item_{0}.{1}.{2}", i, j, k)})
                    Next k
                    item1.Items.Add(item2)
                Next j
                Data.Add(item1)
            Next i
        End Sub
    End Class

    Public Class Item
        Inherits BindableBase

        Public Property Id() As Integer
            Get
                Return GetValue(Of Integer)()
            End Get
            Set(value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Name() As String
            Get
                Return GetValue(Of String)()
            End Get
            Set(value As String)
                SetValue(value)
            End Set
        End Property

        Public ReadOnly Property Items() As ObservableCollection(Of Item)

        Public Sub New()
            Items = New ObservableCollection(Of Item)()
        End Sub
    End Class
End Namespace
