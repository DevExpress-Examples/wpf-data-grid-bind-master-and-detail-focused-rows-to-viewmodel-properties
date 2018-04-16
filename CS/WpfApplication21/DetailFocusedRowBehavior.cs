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
    public class DetailFocusedRowBehavior : Behavior<TableView>
    {
        public static readonly DependencyProperty FocusedRowProperty = DependencyProperty.Register("FocusedRow", typeof(object), typeof(DetailFocusedRowBehavior), new UIPropertyMetadata(null, OnFocusedRowChanged));

        private static void OnFocusedRowChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DetailFocusedRowBehavior detailFocusedRowBehavior = o as DetailFocusedRowBehavior;
            if (detailFocusedRowBehavior != null)
                detailFocusedRowBehavior.OnFocusedRowChanged((object)e.OldValue, (object)e.NewValue);
        }

        protected virtual void OnFocusedRowChanged(object oldValue, object newValue)
        {
            if (isChangeInternal)
            {
                isChangeInternal = false;
                return;
            }
            if (MasterView.MasterRootRowsContainer.FocusedView == MasterView && !MasterGrid.IsMasterRowExpanded(MasterView.FocusedRowHandle))
                MasterGrid.ExpandMasterRow(MasterView.FocusedRowHandle);
            GridControl detailControl = MasterGrid.GetDetail(MasterView.FocusedRowHandle) as GridControl;
            detailControl.View.MoveFocusedRow(detailControl.DataController.FindRowByRowValue(newValue));
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

        TableView MasterView
        {
            get
            {
                return (DetailView.DataControl.OwnerDetailDescriptor.Parent as GridControl).View as TableView;
            }
        }

        TableView DetailView
        {
            get
            {
                return AssociatedObject;
            }
        }

        GridControl MasterGrid
        {
            get
            {
                return DetailView.DataControl.OwnerDetailDescriptor.Parent as GridControl;
            }
        }

        bool isChangeInternal = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            if (DetailView.DataControl.OwnerDetailDescriptor == null)
                throw new Exception("DetailFocusedRowBehavior should be attached to the detail view.");
            DetailView.FocusedRowChanged += DetailViewFocusedRowChanged;
            MasterView.FocusedViewChanged += MasterViewFocusedViewChanged;
            MasterView.FocusedRowChanged += MasterViewFocusedRowChanged;
            MasterGrid.MasterRowExpanded += MasterGridMasterRowExpanded;
        }

        void MasterViewFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
              FocusedRowInternal = null;
        }

        void MasterGridMasterRowExpanded(object sender, RowEventArgs e)
        {
            (MasterGrid.GetDetail(MasterView.FocusedRowHandle) as GridControl).View.MoveFocusedRow(0);
        }

        void MasterViewFocusedViewChanged(object sender, FocusedViewChangedEventArgs e)
        {
            if (e.NewView == MasterView)
            {
                FocusedRowInternal = null;
                return;
            }
            if (e.OldView == MasterView)
                MasterView.FocusedRowHandle = (e.NewView.DataControl as GridControl).GetMasterRowHandle();
            FocusedRowInternal = e.NewView.FocusedRow;
        }

        void DetailViewFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            FocusedRowInternal = e.NewRow;
        }

        protected override void OnDetaching()
        {
            DetailView.FocusedRowChanged -= DetailViewFocusedRowChanged;
            MasterView.FocusedViewChanged -= MasterViewFocusedViewChanged;
            MasterView.FocusedRowChanged -= MasterViewFocusedRowChanged;
            MasterGrid.MasterRowExpanded -= MasterGridMasterRowExpanded;
            base.OnDetaching();
        }
    }
}