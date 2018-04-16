// Developer Express Code Central Example:
// GridControl - How to bind Master and Detail focused rows to ViewModel objects to follow the MVVM pattern in your application
// 
// This example demonstrates how to use ViewModel properties to track and control
// focused row changes both for Master and Detail grids. This capability is
// achieved by creating attached behavior that handles all necessary events
// (especially for the focused view and row changing, both in the detail and master
// grids). The logic of focusing rows can be changed inside this behavior depending
// on your requirements. For example, you may want not to focus the first detail
// row when a master row is expanded. If so, change the MasterRowExpanded event
// handler as follows:
// void MasterGridMasterRowExpanded(object sender,
// RowEventArgs e)
// {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
// GridControl).View.FocusedRow = null;
// }
// So, the main idea of the example is to
// show how the row focusing logic can be defined in the GridControl that operates
// in Master-Detail mode.
// Note that the approach shown in the example will work
// only with the DataControlDetailDescriptor, as other descriptors use custom
// templates to display detail content that is not synchronized with the master
// grid.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4402

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace E4402
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Employee _FocusedEmployee;
        public Employee FocusedEmployee
        {
            get
            {
                return _FocusedEmployee;
            }
            set
            {
                _FocusedEmployee = value;
                RaisePropertyChanged("FocusedEmployee");
            }
        }

        private Order _FocusedOrder;
        public Order FocusedOrder
        {
            get
            {
                return _FocusedOrder;
            }
            set
            {
                _FocusedOrder = value;
                RaisePropertyChanged("FocusedOrder");
            }
        }

        private List<Employee> employees;
        public List<Employee> Employees
        {
            get
            {
                if (employees == null)
                    PopulateEmployees();
                return employees;

            }
        }

        private void PopulateEmployees()
        {
            employees = new List<Employee>();
            employees.Add(new Employee("Bruce", "Cambell", "Sales Manager", "Education includes a BA in psychology from Colorado State University in 1970.  He also completed 'The Art of the Cold Call.'  Bruce is a member of Toastmasters International."));
            employees[0].Orders.Add(new Order("Supplier 1", DateTime.Now, "TV", 20));
            employees[0].Orders.Add(new Order("Supplier 2", DateTime.Now.AddDays(3), "Projector", 15));
            employees[0].Orders.Add(new Order("Supplier 3", DateTime.Now.AddDays(3), "HDMI Cable", 50));
            employees.Add(new Employee("Cindy", "Haneline", "Vice President of Sales", "Cindy received her BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  She is fluent in French and Italian and reads German.  She joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Cindy is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."));
            employees[1].Orders.Add(new Order("Supplier 1", DateTime.Now.AddDays(1), "Blu-Ray Player", 10));
            employees[1].Orders.Add(new Order("Supplier 2", DateTime.Now.AddDays(1), "Blu-Ray Player", 10));
            employees[1].Orders.Add(new Order("Supplier 3", DateTime.Now.AddDays(1), "Blu-Ray Player", 10));
            employees[1].Orders.Add(new Order("Supplier 4", DateTime.Now.AddDays(1), "Blu-Ray Player", 10));
            employees.Add(new Employee("Jack", "Lee", "Sales Manager", "Education includes a BA in psychology from Colorado State University in 1970.  He also completed 'The Art of the Cold Call.'  Jack is a member of Toastmasters International."));
            employees[2].Orders.Add(new Order("Supplier 1", DateTime.Now, "AV Receiver", 20));
            employees[2].Orders.Add(new Order("Supplier 2", DateTime.Now.AddDays(3), "Projector", 15));
            employees.Add(new Employee("Cindy", "Johnson", "Vice President of Sales", "Cindy received her BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  She is fluent in French and Italian and reads German.  She joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Cindy is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association."));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Employee
    {
        private List<Order> orders;
        public Employee(string firstName, string lastName, string title, string notes)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Title = title;
            this.Notes = notes;
            this.orders = new List<Order>();
        }
        public Employee() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public List<Order> Orders { get { return orders; } }
        public override string ToString()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }
    }

    public class Order
    {
        public Order(string supplier, DateTime date, string productName, int quantity)
        {
            this.Supplier = supplier;
            this.Date = date;
            this.ProductName = productName;
            this.Quantity = quantity;
        }
        public string Supplier { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            return String.Format("{0} - {1}", Supplier, ProductName);
        }
    }
}
