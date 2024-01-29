using AutoWarehouse.DateBase;
using AutoWarehouse.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoWarehouse.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeOrderWindow.xaml
    /// </summary>
    public partial class ChangeOrderWindow : Window
    {
        private OrderEquipment equipment = new OrderEquipment();
        public ChangeOrderWindow(OrderEquipment chousenOrder)
        {
            InitializeComponent();
            DataContext = equipment;
            DataCombo();

            foreach (var item in App.Current.Windows)
            {
                if (item is OrderWindow)
                {
                    Owner = (Window)item;
                }
            }
            if(chousenOrder != null)
            {
                DataContext = chousenOrder;
            }
            else
            {
                var newOrder = new OrderEquipment();
                newOrder.Worker = new User().Id;
                newOrder.Category = new Category().Id;
                DataContext = newOrder;
            }

        }

        public void SelectedCombo()
        {
            (DataContext as OrderEquipment).Worker = (cmbUser.SelectedItem as User).Id;
            (DataContext as OrderEquipment).Category = (cmbCategory.SelectedItem as Category).Id;
        }

        public void DataCombo()
        {
            using (var db = new WarehouseDB())
            {
                cmbCategory.ItemsSource = db.Category.ToList();
                cmbUser.ItemsSource = db.User.ToList();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SelectedCombo();
            try
            {
                using (var db = new WarehouseDB())
                {
                    db.OrderEquipment.AddOrUpdate(DataContext as OrderEquipment);
                    db.SaveChanges();
                    ((Owner as OrderWindow).DataContext as AppVM).LoadDate();
                    Close();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка в редактировании или добавлении данных пользователя!");
            }
        }
    }
}
