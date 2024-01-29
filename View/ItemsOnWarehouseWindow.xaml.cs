using AutoWarehouse.DateBase;
using AutoWarehouse.VM;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ItemsOnWarehouseWindow.xaml
    /// </summary>
    public partial class ItemsOnWarehouseWindow : Window
    {
        public ItemsOnWarehouseWindow()
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
                    var idForDelete = (DataContext as AppVM).SelectItem.Id;

                    var objectForDelete = db.Item.FirstOrDefault(x => x.Id == idForDelete);

                    db.Item.Remove(objectForDelete);

                    db.SaveChanges();

                    (DataContext as AppVM).LoadDate();
                }
            }
            catch
            {

            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (DataContext as AppVM).SearchItem((sender as TextBox).Text);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show(); Close();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show(); Close();
        }
    }
}
