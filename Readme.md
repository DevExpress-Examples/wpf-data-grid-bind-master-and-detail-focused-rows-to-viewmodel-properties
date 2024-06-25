<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128647419/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4402)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [UpdateMasterDetailFocusedRowBehavior.cs](./CS/UpdateMasterDetailFocusedRowBehavior.cs) (VB: [UpdateMasterDetailFocusedRowBehavior.vb](./VB/UpdateMasterDetailFocusedRowBehavior.vb))
* [ViewModel.cs](./CS/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel.vb))
* [Window1.xaml](./CS/Window1.xaml) (VB: [Window1.xaml](./VB/Window1.xaml))
* [Window1.xaml.cs](./CS/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/Window1.xaml.vb))
<!-- default file list end -->
# Binding Master and Detail focused rows to ViewModel objects


<p>This example demonstrates how to use ViewModel properties to track and control focused row changes both for Master and Detail grids. This capability is achieved by creating attached behavior that handles all necessary events (especially for the focused view and row changing, both in the detail and master grids). </p>
<p>So, the main idea of the example is to show how the row focusing logic can be defined in the GridControl that operates in Master-Detail mode.</p>
<p><strong><em>I</em></strong><strong><em>mportant n</em></strong><strong><em>ote</em></strong><strong><em>: </em></strong><em>t</em><em>he approach shown in the example will work only with the </em><strong><em>DataControlDetailDescriptor</em></strong><em>, as other descriptors use custom templates to display detail content that is not synchronized with the master grid.<br><br></em></p>
<p><em>Please note that this approach is </em><strong><em>outdated</em></strong><em> starting with version 15.1, where the capability to bind selection of the master and detail grids was supported out of the box. Starting with that version, bind the CurrentItem/SelectedItem/SelectedItems properties directly as described at </em><a href="https://www.devexpress.com/Support/Center/p/T106654"><em>Master-detail grid - Add the capability to bind selection and navigation properties</em></a><em>.</em></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-bind-master-and-detail-focused-rows-to-viewmodel-properties&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-bind-master-and-detail-focused-rows-to-viewmodel-properties&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
