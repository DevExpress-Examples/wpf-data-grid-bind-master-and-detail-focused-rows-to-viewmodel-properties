using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace MasterDetailInside {
    public class ViewModel : BindableBase {
        public Item Level1CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
        public Item Level2CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
        public Item Level3CurrentItem { get { return GetValue<Item>(); } set { SetValue(value); } }
        public ObservableCollection<Item> Data { get; }
        public ViewModel() {
            Data = new ObservableCollection<Item>();
            for (int i = 0; i < 50; i++) {
                Item item1 = new Item() { Id = i, Name = string.Format("Item_{0}", i), };
                for (int j = 0; j < 10; j++) {
                    Item item2 = new Item() { Id = j, Name = string.Format("Item_{0}.{1}", i, j) };
                    for (int k = 0; k < 5; k++) {
                        item2.Items.Add(new Item() { Id = k, Name = string.Format("Item_{0}.{1}.{2}", i, j, k) });
                    }
                    item1.Items.Add(item2);
                }
                Data.Add(item1);
            }
        }
    }
    public class Item : BindableBase {
        public int Id { get { return GetValue<int>(); } set { SetValue(value); } }
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public ObservableCollection<Item> Items { get; }
        public Item() {
            Items = new ObservableCollection<Item>();
        }
    }
}