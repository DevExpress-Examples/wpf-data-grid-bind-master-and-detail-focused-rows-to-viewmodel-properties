<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128647419/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4402)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
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
