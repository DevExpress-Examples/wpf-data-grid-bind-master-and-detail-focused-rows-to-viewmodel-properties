<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128647419/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4402)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->

# How to bind Master and Detail grid focused rows to ViewModel

You can bind [CurrentItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.CurrentItem)/[SelectedItem](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.SelectedItem)/[SelectedItems](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.SelectedItems) for master and detail grids to different properties within your ViewModel.

For example:

```xml
<Window.DataContext>
    <local:ViewModel />
</Window.DataContext>
...
<dxg:GridControl ... CurrentItem="{Binding Level1CurrentItem}" ItemsSource="{Binding Data}">
    <dxg:GridControl.DetailDescriptor>
        <dxg:DataControlDetailDescriptor ItemsSourceBinding="...">
            <dxg:GridControl ... CurrentItem="{Binding Level2CurrentItem}">
                <dxg:GridControl.DetailDescriptor>
                    <dxg:DataControlDetailDescriptor ItemsSourceBinding="...">
                        <dxg:GridControl ... CurrentItem="{Binding Level3CurrentItem}" />
                    </dxg:DataControlDetailDescriptor>
                </dxg:GridControl.DetailDescriptor>
            </dxg:GridControl>
        </dxg:DataControlDetailDescriptor>
    </dxg:GridControl.DetailDescriptor>
</dxg:GridControl>
```

```cs
public class ViewModel : BindableBase {
    public Item Level1CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
    public Item Level2CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
    public Item Level3CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
    public ObservableCollection<Item> Data { get; set; }
    ...
}
```
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-bind-master-and-detail-focused-rows-to-viewmodel-properties&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-bind-master-and-detail-focused-rows-to-viewmodel-properties&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
