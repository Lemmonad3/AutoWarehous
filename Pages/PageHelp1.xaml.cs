using AutoWarehouse.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoWarehouse.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageHelp1.xaml
    /// </summary>
    public partial class PageHelp1 : Page
    {
        public PageHelp1()
        {
            InitializeComponent();
        }


        private void btnGoMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
          NavigationService.Navigate(new PageHelp2());
        }
    }
}
