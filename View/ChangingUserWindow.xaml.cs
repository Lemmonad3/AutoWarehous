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
    /// Логика взаимодействия для ChangingUserWindow.xaml
    /// </summary>
    public partial class ChangingUserWindow : Window
    {
        private User user = new User(); 
        public ChangingUserWindow(User chousenUser)
        {
            InitializeComponent();
            DataContext = user;
            DateComboBox();

            foreach(var item in App.Current.Windows) 
            {
                if(item is AdminWindow) 
                {
                    Owner = (Window)item;
                }
            }
            if (chousenUser != null)
            {
                DataContext = chousenUser;
            }
            else
            {
                var newUser = new User();   
                newUser.UserRole = new Role().Id;
                DataContext  = newUser; 

            }
        }

        public void SelectedCombo()
        {
            (DataContext as User).UserRole = (cmbRole.SelectedItem as Role).Id;

        }

        public void DateComboBox()
        {
            using (var db = new WarehouseDB())
            {
                cmbRole.ItemsSource = db.Role.ToList();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SelectedCombo();
            try
            {
                using (var db = new WarehouseDB())
                {
                    db.User.AddOrUpdate(DataContext as User);
                    db.SaveChanges();
                    ((Owner as AdminWindow).DataContext as AppVM).LoadDate();
                    Close();

                }
            }
            catch
            {
                MessageBox.Show("Ошибка в редактировании или добавлении данных!");
            }
        }
    }
}
