using AutoWarehouse.DateBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWarehouse.VM
{
    public class AppVM : BaseVM
    {
        private User _selectUser;
        public User SelectUser
        {
            get => _selectUser;
            set
            {
                _selectUser = value;
                onPropertyChanged(nameof(SelectUser));
            }
        }
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                onPropertyChanged(nameof(Users));
            }
        }

        private Item _item;
        public Item SelectItem
        {
            get => _item;
            set
            {
                _item = value;
                onPropertyChanged(nameof(SelectItem));
            }
        }
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                onPropertyChanged(nameof(Items));
            }
        }

        private OrderEquipment _orderEquipment;
        public OrderEquipment SelectOrder
        {
            get => _orderEquipment;
            set
            {
                _orderEquipment = value;
                onPropertyChanged(nameof(SelectOrder));
            }
        }

        private ObservableCollection<OrderEquipment> _orders;
        public ObservableCollection<OrderEquipment> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                onPropertyChanged(nameof(Orders));
            }
        }

        public AppVM()
        {
            Orders = new ObservableCollection<OrderEquipment>();
            Items = new ObservableCollection<Item>();
            Users = new ObservableCollection<User>();
            LoadDate();
        }

        public void LoadDate()
        {
            Orders.Clear();
            Items.Clear();
            Users.Clear();
            using (WarehouseDB db = new WarehouseDB())
            {
                var userList = db.User.Include("Role").ToList();
                userList.ForEach(p => Users.Add(p));

                var itemList = db.Item.Include("Category").Include("Picture").ToList();
                itemList.ForEach(i => Items.Add(i));

                var orderList = db.OrderEquipment.Include("Category1").Include("User").ToList();
                orderList.ForEach(o => Orders.Add(o));
            }
        }

        public void Search(string quntint)
        {
            if (string.IsNullOrEmpty(quntint)) { LoadDate(); return; }

            Users.Clear();
            using (var db = new WarehouseDB())
            {
                var result = db.User.Where(u => u.Name.ToLower().Contains(quntint.ToLower())
                || u.Surname.ToLower().Contains(quntint.ToLower())).Include("Role").ToList();

                result.ForEach(u => Users.Add(u));
            }

        }

        public void SearchItem(string quint)
        {
            if (string.IsNullOrEmpty(quint)) { LoadDate(); return; }
            Items.Clear();
            using (var db = new WarehouseDB())
            {
                var result = db.Item.Where(i => i.NameItem.ToLower().Contains(quint.ToLower())).Include("Category").Include("Picture").ToList();
                result.ForEach(i => Items.Add(i));
            }
        }
    }
}
