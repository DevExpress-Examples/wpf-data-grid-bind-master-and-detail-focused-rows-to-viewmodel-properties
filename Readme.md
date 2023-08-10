<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128647419/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4402)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WPF Data Grid - Bind Master and Detail Focused Rows to View Model Properties

This example binds focused rows in master and detail grids to View Model properties. The focused row's name for each nested level is displayed below the [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl):

![image](https://user-images.githubusercontent.com/65009440/221548801-e8f34114-aa55-4445-b36d-d3f2ebf1d242.png)

You can bind [CurrentItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.CurrentItem), [SelectedItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.SelectedItem), and [SelectedItems](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.SelectedItems) properties for master and detail grids to View Model properties.

## Files to Review

* [Window1.xaml](./CS/Window1.xaml) (VB: [Window1.xaml](./VB/Window1.xaml))
* [ViewModel.cs](./CS/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel.vb))

## Documentation

* [Data Grid in Details](https://docs.devexpress.com/WPF/119851/controls-and-libraries/data-grid/master-detail/data-grid-in-details)
* [CurrentItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.CurrentItem)
* [DataControlDetailDescriptor](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlDetailDescriptor)
* [Master-Detail Mode Limitations](https://docs.devexpress.com/WPF/11841/controls-and-libraries/data-grid/master-detail/master-detail-mode-limitations)

## More Examples

* [WPF Data Grid - Create Master-Detail Grid](https://github.com/DevExpress-Examples/wpf-data-grid-create-master-detail-grid)
* [WPF Data Grid - Display Detail Content in Tabs](https://github.com/DevExpress-Examples/wpf-data-grid-display-detail-content-in-tabs)
* [WPF Data Grid - Expand and Collapse Master Rows](https://github.com/DevExpress-Examples/wpf-data-grid-expand-and-collapse-master-rows)
* [WPF Data Grid - Display Different Details Based on Data in the Master Row](https://github.com/DevExpress-Examples/wpf-data-grid-display-different-details-based-on-master-row-data)
* [WPF Data Grid - Use the ParentPath Property to Define the Selected Detail Row in the View Model](https://github.com/DevExpress-Examples/how-to-use-the-parentpath-property-to-enable-the-binding-from-the-viewmodel-to-grid-t291661)
