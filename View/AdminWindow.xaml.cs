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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = new AppVM();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (DataContext as AppVM).Search((sender as TextBox).Text);
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new WarehouseDB())
                {
                    var idForDelete = (DataContext as AppVM).SelectUser.Id;

                    var objectForDelete = db.User.FirstOrDefault(x => x.Id == idForDelete);

                    db.User.Remove(objectForDelete);

                    db.SaveChanges();

                    (DataContext as AppVM).LoadDate();
                }
            }
            catch
            {

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var changeUser = new ChangingUserWindow((DataContext as AppVM).SelectUser);
            changeUser.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show(); Close();
        }
    }
}
