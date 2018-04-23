// Developer Express Code Central Example:
// Binding Master and Detail focused rows to ViewModel objects
// 
// This example demonstrates how to use ViewModel properties to track and control
// focused row changes both for Master and Detail grids. This capability is
// achieved by creating attached behavior that handles all necessary events
// (especially for the focused view and row changing, both in the detail and master
// grids). The logic of focusing rows can be changed inside this behavior depending
// on your requirements. For example, you may want not to focus the first detail
// row when a master row is expanded. If so, change the MasterRowExpanded event
// handler as follows:
// 
// void MasterGridMasterRowExpanded(object sender,
// RowEventArgs e)
// {  (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as
// GridControl).View.FocusedRow = null;
// }
// 
// 
// 
// 
// So, the main idea of the example is
// to show how the row focusing logic can be defined in the GridControl that
// operates in Master-Detail mode.
// 
// Important note:the approach shown in the
// example will work only with the DataControlDetailDescriptor, as other
// descriptors use custom templates to display detail content that is not
// synchronized with the master grid.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4402


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MasterDetailInside
{
    public class ViewModel// : INotifyPropertyChanged
    {
        List<ParentTestData> data;
        public List<ParentTestData> Data
        {
            get
            {
                if (data == null)
                {
                    data = new List<ParentTestData>();
                    for (int i = 0; i < 50; i++)
                    {
                        ParentTestData parentTestData = new ParentTestData() { Text = "Master" + i, Number = i, Data = new List<TestData>() };
                        for (int j = 0; j < 10; j++)
                        {
                            TestData testData = new TestData() { Text = "Detail" + j + " Master" + i, Number = j, Ready = j % 2 != 0, Data = new List<DetailTestData>() };
                            for (int k = 0; k < 5; k++)
                            {
                                testData.Data.Add(new DetailTestData() { Text = "NestedDetail" + k + " Master" + j, Number = k, Ready = j % 2 != 0 });
                            }
                            parentTestData.Data.Add(testData);
                        }
                        data.Add(parentTestData);
                    }
                }
                return data;
            }
        }

    }

    public class DetailTestData : IText
    {
        public bool Ready { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
    }

    public class TestData : IText
    {
        public bool Ready { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public List<DetailTestData> Data { get; set; }
    }
    public class ParentTestData : IText
    {
        public string Text { get; set; }
        public int Number { get; set; }
        public List<TestData> Data { get; set; }
    }

    interface IText
    {
        string Text
        {
            get;
            set;
        }
    }
}
