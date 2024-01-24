using AutoWarehouse.DateBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoWarehouse.VM
{
    public class AutoVM: BaseVM
    {
        private string _Login;
        private string _Password;
        
        public string Login
        {
            get => _Login;
            set
            {
                _Login = value;
                onPropertyChanged(nameof(Login));
            }
        }
        public string Password
        { 
            get => _Password;
            set
            {
                _Password = value;
                onPropertyChanged(nameof(Password));
            }
        }
        public async Task<bool> ValidateUserLoginAndPassword()
        {
            try
            {
                using(var context = new WarehouseDB())
                {
                    var user = await context.User.FirstOrDefaultAsync(u => u.Login == _Login && u.Password == _Password);
                    if (user!= null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка соединения...", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
