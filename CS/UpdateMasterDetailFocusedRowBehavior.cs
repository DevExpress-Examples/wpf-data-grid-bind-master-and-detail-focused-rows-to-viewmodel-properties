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

using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MasterDetailInside
{
    public class UpdateMasterDetailFocusedRowBehavior : Behavior<TableView>
    {
        public static readonly DependencyProperty CurrentItemProperty = DependencyProperty.RegisterAttached("CurrentItem", typeof(object), typeof(UpdateMasterDetailFocusedRowBehavior), new FrameworkPropertyMetadata(CurrentItemPropertyChanged));
        public static void SetCurrentItem(UIElement element, object value)
        {
            element.SetValue(CurrentItemProperty, value);
        }
        public static object GetCurrentItem(UIElement element)
        {
            return (object)element.GetValue(CurrentItemProperty);
        }


        public static readonly DependencyProperty MasterGridBehaviorProperty = DependencyProperty.RegisterAttached("MasterGridBehavior", typeof(UpdateMasterDetailFocusedRowBehavior), typeof(UpdateMasterDetailFocusedRowBehavior), null);
        public static void SetMasterGridBehavior(UIElement element, UpdateMasterDetailFocusedRowBehavior value)
        {
            element.SetValue(MasterGridBehaviorProperty, value);
        }
        public static UpdateMasterDetailFocusedRowBehavior GetMasterGridBehavior(UIElement element)
        {
            return (UpdateMasterDetailFocusedRowBehavior)element.GetValue(MasterGridBehaviorProperty);
        }

        public static readonly DependencyProperty GridNestingLevelProperty = DependencyProperty.RegisterAttached("GridNestingLevel", typeof(int), typeof(UpdateMasterDetailFocusedRowBehavior), null);
        public static void SetGridNestingLevel(UIElement element, int value)
        {
            element.SetValue(GridNestingLevelProperty, value);
        }
        public static int GetGridNestingLevel(UIElement element)
        {
            return (int)element.GetValue(GridNestingLevelProperty);
        }

        Dictionary<int, object> levelValue = new Dictionary<int, object>();
        int boundCurrentItemLockCount = 0;
        int gridCurrentItemLockCount = 0;
        int maxNestingLevel = 0;
        protected override void OnAttached()
        {
            maxNestingLevel = GetMaxNestingLevel();
            InitGridEventsAndProperties((GridControl)AssociatedObject.DataControl, 0);
            AssociatedObject.FocusedViewChanged += View_FocusedViewChanged;
        }
        int GetMaxNestingLevel()
        {
            int nestingLevel = 0;
            GridControl tempGrid = (GridControl)AssociatedObject.DataControl;
            while (true)
            {
                DataControlDetailDescriptor detailDescriptor = tempGrid.DetailDescriptor as DataControlDetailDescriptor;
                if (detailDescriptor != null)
                {
                    tempGrid = (GridControl)detailDescriptor.DataControl;
                    nestingLevel++;
                }
                else
                    break;
            }
            return nestingLevel;
        }
        private static void CurrentItemPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            GridControl grid = source as GridControl;
            UpdateMasterDetailFocusedRowBehavior behavior = UpdateMasterDetailFocusedRowBehavior.GetMasterGridBehavior(grid);
            if (behavior == null)
                return;
            if (behavior.boundCurrentItemLockCount == 0)
            {
                behavior.BoundCurrentItemChanged(grid, e.NewValue);
            }
        }
        void BoundCurrentItemChanged(GridControl virtualGrid, object newCurrentItem)
        {
            int GridNestingLevel = GetGridNestingLevel(virtualGrid);

            levelValue[GridNestingLevel] = newCurrentItem;
            GridControl visualGrid = (GridControl)AssociatedObject.DataControl;
            GridControl tempGrid;

            gridCurrentItemLockCount++;
            for (int i = 0; i < GridNestingLevel; i++)
            {
                int rowHandle = visualGrid.DataController.FindRowByRowValue(levelValue[i]);
                tempGrid = (GridControl)visualGrid.GetDetail(rowHandle);
                if (tempGrid == null)
                {
                    if (newCurrentItem == null)
                        return;
                    visualGrid.ExpandMasterRow(rowHandle);
                    tempGrid = (GridControl)visualGrid.GetDetail(rowHandle);
                    if (tempGrid == null)
                        return;
                }
                visualGrid = tempGrid;
            }
            int newFocusedRowHandle = visualGrid.DataController.FindRowByRowValue(newCurrentItem);
            visualGrid.View.MoveFocusedRow(newFocusedRowHandle);
            gridCurrentItemLockCount--;
        }
        void InitGridEventsAndProperties(GridControl grid, int level)
        {
            grid.CurrentItemChanged += grid_CurrentItemChanged;
            grid.SetValue(UpdateMasterDetailFocusedRowBehavior.MasterGridBehaviorProperty, this);
            grid.SetValue(UpdateMasterDetailFocusedRowBehavior.GridNestingLevelProperty, level);
            DataControlDetailDescriptor detailDescriptor = grid.DetailDescriptor as DataControlDetailDescriptor;
            if (detailDescriptor != null)
                InitGridEventsAndProperties((GridControl)detailDescriptor.DataControl, level + 1);

        }

        void View_FocusedViewChanged(object sender, FocusedViewChangedEventArgs e)
        {
            if (gridCurrentItemLockCount == 0)
                InvalidateCurrentRow();
        }

        void grid_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (gridCurrentItemLockCount == 0)
            {
                int gridLevel = GetGridNestingLevel((GridControl)sender);
                for (int i = gridLevel + 1; i <= maxNestingLevel; i++)
                    levelValue[i] = null;
                InvalidateCurrentRow();
            }
        }

        void InvalidateCurrentRow()
        {
            GridControl focusedGrid = (GridControl)AssociatedObject.FocusedView.DataControl;

            UpdateCurrentItems(focusedGrid, GetVisualGridNestingLevel(focusedGrid, 0), focusedGrid.CurrentItem);
            UpdateCurrentItemAttachedPropertyValue((GridControl)AssociatedObject.DataControl, 0);
        }
        void UpdateCurrentItems(GridControl grid, int level, object levelCurrentItem) {
            var toModify = levelValue.Where(i => i.Key > level).ToList();
            foreach (KeyValuePair<int, object> item in toModify)
                levelValue[item.Key] = null;

            UpdateCurrentItem(grid, level, levelCurrentItem);
        }
        void UpdateCurrentItem(GridControl grid, int level, object levelCurrentItem)
        {
            levelValue[level] = levelCurrentItem;

            GridControl masterGrid = grid.GetMasterGrid();
            if (masterGrid != null)
                UpdateCurrentItem(masterGrid, level - 1, masterGrid.GetRow(grid.GetMasterRowHandle()));
        }
        void UpdateCurrentItemAttachedPropertyValue(GridControl grid, int level)
        {
            object currentItemValue;
            if (!levelValue.TryGetValue(level, out currentItemValue))
                return;
            boundCurrentItemLockCount++;
            grid.SetCurrentValue(UpdateMasterDetailFocusedRowBehavior.CurrentItemProperty, currentItemValue);
            boundCurrentItemLockCount--;

            DataControlDetailDescriptor detailDescriptor = grid.DetailDescriptor as DataControlDetailDescriptor;
            if (detailDescriptor != null)
                UpdateCurrentItemAttachedPropertyValue((GridControl)detailDescriptor.DataControl, level + 1);
        }

        int GetVisualGridNestingLevel(GridControl grid, int startLevel)
        {
            GridControl masterGrid = grid.GetMasterGrid();
            if (masterGrid != null)
                return GetVisualGridNestingLevel(masterGrid, startLevel + 1);
            return startLevel;
        }

    }
}
