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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        public OrderWindow()
        {
            InitializeComponent();
            DataContext = new AppVM();
           
        }  

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new WarehouseDB())
                {
                    var idForDelete = (DataContext as AppVM).SelectOrder.Id;

                    var objectForDelete = db.OrderEquipment.FirstOrDefault(x => x.Id == idForDelete);

                    db.OrderEquipment.Remove(objectForDelete);

                    db.SaveChanges();

                    (DataContext as AppVM).LoadDate();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при удалении!");
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            var changeOrder = new ChangeOrderWindow((DataContext as AppVM).SelectOrder);
            changeOrder.Show();


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new ItemsOnWarehouseWindow().Show(); Close();
        }
    }
}
