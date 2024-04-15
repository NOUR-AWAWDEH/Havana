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
using Havana;
using Library.Models.Classes;
using Library.Models.Products.Snacks.Hot;
using Microsoft.SqlServer.Server;

namespace Havana.Snaks
{
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class Hot : Window
    {
        public  static bool isAdd { get; set; }
        public Hot()
        {
            InitializeComponent();

        }

        private void BackToOrder(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.BackToOrderWindow();
            this.Close();
        }

        private void AddGrilledChickenButt(object sender, RoutedEventArgs e)
        {

            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(7);

        }

        private void AddGrilledMozzarellaButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(9);

        }

        private void AddChesseAndTomatoButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(11);
        }

        private void AddHotDogButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.AddSnackItem(10);
        }
    }
}
