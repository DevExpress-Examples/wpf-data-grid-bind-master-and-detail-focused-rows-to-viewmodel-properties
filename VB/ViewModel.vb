Imports DevExpress.Mvvm
Imports System.Collections.ObjectModel

Namespace MasterDetailInside

    Public Class ViewModel
        Inherits BindableBase

        Public Property Level1CurrentItem As Item
            Get
                Return GetValue(Of Item)()
            End Get

            Set(ByVal value As Item)
                SetValue(value)
            End Set
        End Property

        Public Property Level2CurrentItem As Item
            Get
                Return GetValue(Of Item)()
            End Get

            Set(ByVal value As Item)
                SetValue(value)
            End Set
        End Property

        Public Property Level3CurrentItem As Item
            Get
                Return GetValue(Of Item)()
            End Get

            Set(ByVal value As Item)
                SetValue(value)
            End Set
        End Property

        Public ReadOnly Property Data As ObservableCollection(Of Item)

        Public Sub New()
            Data = New ObservableCollection(Of Item)()
            For i As Integer = 0 To 50 - 1
                Dim item1 As Item = New Item() With {.Id = i, .Name = String.Format("Item_{0}", i)}
                For j As Integer = 0 To 10 - 1
                    Dim item2 As Item = New Item() With {.Id = j, .Name = String.Format("Item_{0}.{1}", i, j)}
                    For k As Integer = 0 To 5 - 1
                        item2.Items.Add(New Item() With {.Id = k, .Name = String.Format("Item_{0}.{1}.{2}", i, j, k)})
                    Next

                    item1.Items.Add(item2)
                Next

                Data.Add(item1)
            Next
        End Sub
    End Class

    Public Class Item
        Inherits BindableBase

        Public Property Id As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Name As String
            Get
                Return GetValue(Of String)()
            End Get

            Set(ByVal value As String)
                SetValue(value)
            End Set
        End Property

        Public ReadOnly Property Items As ObservableCollection(Of Item)

        Public Sub New()
            Items = New ObservableCollection(Of Item)()
        End Sub
    End Class
End Namespace
