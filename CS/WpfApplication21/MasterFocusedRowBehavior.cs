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
using System.Windows;
using DevExpress.Xpf.Grid;
using System.Windows.Interactivity;

namespace E4402
{
    public class MasterFocusedRowBehavior : Behavior<TableView>
    {
        public static readonly DependencyProperty FocusedRowProperty = DependencyProperty.Register("FocusedRow", typeof(object), typeof(MasterFocusedRowBehavior), new UIPropertyMetadata(null, OnFocusedRowChanged));

        private static void OnFocusedRowChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MasterFocusedRowBehavior masterFocusedRowBehavior = o as MasterFocusedRowBehavior;
            if (masterFocusedRowBehavior != null)
                masterFocusedRowBehavior.OnFocusedRowChanged((object)e.OldValue, (object)e.NewValue);
        }

        protected virtual void OnFocusedRowChanged(object oldValue, object newValue)
        {
            if (isChangeInternal)
            {
                isChangeInternal = false;
                return;
            }
            AssociatedObject.MoveFocusedRow((AssociatedObject.DataControl as GridControl).DataController.FindRowByRowValue(newValue));
        }

        public object FocusedRow
        {
            get
            {
                return (object)GetValue(FocusedRowProperty);
            }
            set
            {
                SetValue(FocusedRowProperty, value);
            }
        }

        object FocusedRowInternal
        {
            get
            {
                return FocusedRow;
            }
            set
            {
                if (FocusedRow == value)
                    return;
                isChangeInternal = true;
                FocusedRow = value;
            }
        }

        bool isChangeInternal = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.FocusedRowChanged += AssociatedObject_FocusedRowChanged;
            AssociatedObject.FocusedViewChanged += AssociatedObject_FocusedViewChanged;
        }

        void AssociatedObject_FocusedViewChanged(object sender, FocusedViewChangedEventArgs e)
        {
            AssociatedObject.FocusedRowHandle = (e.NewView.DataControl as GridControl).GetMasterRowHandle();
        }

        void AssociatedObject_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            FocusedRowInternal = e.NewRow;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.FocusedRowChanged -= AssociatedObject_FocusedRowChanged;
            base.OnDetaching();
        }
    }
}
