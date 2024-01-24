using AutoWarehouse.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AutoWindow.xaml
    /// </summary>
    public partial class AutoWindow : Window
    {
        public AutoWindow()
        {
            InitializeComponent();
            AppDateBase.db = new DateBase.WarehouseDB();
            DataContext = new AppVM();
        }

        private void btnSingin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = AppDateBase.db.User.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == tbPassword.Password);
                if (user == null) 
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
                if(user.UserRole == 1)
                {
                    new AdminWindow().Show(); Close();
                }
                else
                {
                    new ItemsOnWarehouseWindow().Show(); Close();
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Ошибка подключения к базе данных!");
            }
        }
    }
}
