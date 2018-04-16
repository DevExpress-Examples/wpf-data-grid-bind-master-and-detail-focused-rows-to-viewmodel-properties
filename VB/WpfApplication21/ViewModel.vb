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
Imports System.ComponentModel

Namespace E4402
	Public Class ViewModel
		Implements INotifyPropertyChanged
		Private _FocusedEmployee As Employee
		Public Property FocusedEmployee() As Employee
			Get
				Return _FocusedEmployee
			End Get
			Set(ByVal value As Employee)
				_FocusedEmployee = value
				RaisePropertyChanged("FocusedEmployee")
			End Set
		End Property

		Private _FocusedOrder As Order
		Public Property FocusedOrder() As Order
			Get
				Return _FocusedOrder
			End Get
			Set(ByVal value As Order)
				_FocusedOrder = value
				RaisePropertyChanged("FocusedOrder")
			End Set
		End Property

		Private employees_Renamed As List(Of Employee)
		Public ReadOnly Property Employees() As List(Of Employee)
			Get
				If employees_Renamed Is Nothing Then
					PopulateEmployees()
				End If
				Return employees_Renamed

			End Get
		End Property

		Private Sub PopulateEmployees()
			employees_Renamed = New List(Of Employee)()
			employees_Renamed.Add(New Employee("Bruce", "Cambell", "Sales Manager", "Education includes a BA in psychology from Colorado State University in 1970.  He also completed 'The Art of the Cold Call.'  Bruce is a member of Toastmasters International."))
			employees_Renamed(0).Orders.Add(New Order("Supplier 1", DateTime.Now, "TV", 20))
			employees_Renamed(0).Orders.Add(New Order("Supplier 2", DateTime.Now.AddDays(3), "Projector", 15))
			employees_Renamed(0).Orders.Add(New Order("Supplier 3", DateTime.Now.AddDays(3), "HDMI Cable", 50))
			employees_Renamed.Add(New Employee("Cindy", "Haneline", "Vice President of Sales", "Cindy received her BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  She is fluent in French and Italian and reads German.  She joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Cindy is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."))
			employees_Renamed(1).Orders.Add(New Order("Supplier 1", DateTime.Now.AddDays(1), "Blu-Ray Player", 10))
			employees_Renamed(1).Orders.Add(New Order("Supplier 2", DateTime.Now.AddDays(1), "Blu-Ray Player", 10))
			employees_Renamed(1).Orders.Add(New Order("Supplier 3", DateTime.Now.AddDays(1), "Blu-Ray Player", 10))
			employees_Renamed(1).Orders.Add(New Order("Supplier 4", DateTime.Now.AddDays(1), "Blu-Ray Player", 10))
			employees_Renamed.Add(New Employee("Jack", "Lee", "Sales Manager", "Education includes a BA in psychology from Colorado State University in 1970.  He also completed 'The Art of the Cold Call.'  Jack is a member of Toastmasters International."))
			employees_Renamed(2).Orders.Add(New Order("Supplier 1", DateTime.Now, "AV Receiver", 20))
			employees_Renamed(2).Orders.Add(New Order("Supplier 2", DateTime.Now.AddDays(3), "Projector", 15))
			employees_Renamed.Add(New Employee("Cindy", "Johnson", "Vice President of Sales", "Cindy received her BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  She is fluent in French and Italian and reads German.  She joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Cindy is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."))
		End Sub

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub RaisePropertyChanged(ByVal name As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
		End Sub
	End Class

	Public Class Employee
		Private orders_Renamed As List(Of Order)
		Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal title As String, ByVal notes As String)
			Me.FirstName = firstName
			Me.LastName = lastName
			Me.Title = title
			Me.Notes = notes
			Me.orders_Renamed = New List(Of Order)()
		End Sub
		Public Sub New()
		End Sub
		Public Property FirstName() As String
		Public Property LastName() As String
		Public Property Title() As String
		Public Property Notes() As String
		Public ReadOnly Property Orders() As List(Of Order)
			Get
				Return orders_Renamed
			End Get
		End Property
		Public Overrides Function ToString() As String
			Return String.Format("{0} {1}", FirstName, LastName)
		End Function
	End Class

	Public Class Order
		Public Sub New(ByVal supplier As String, ByVal [date] As DateTime, ByVal productName As String, ByVal quantity As Integer)
			Me.Supplier = supplier
            Me.OrderDate = [date]
			Me.ProductName = productName
			Me.Quantity = quantity
		End Sub
		Public Property Supplier() As String
        Public Property OrderDate() As DateTime
		Public Property ProductName() As String
		Public Property Quantity() As Integer
		Public Overrides Function ToString() As String
			Return String.Format("{0} - {1}", Supplier, ProductName)
		End Function
	End Class
End Namespace
