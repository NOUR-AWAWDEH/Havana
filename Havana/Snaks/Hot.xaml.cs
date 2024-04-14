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

namespace Havana.Snaks
{
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class Hot : Window
    {
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

        }

        private void AddGrilledMozzarellaButt(object sender, RoutedEventArgs e)
        {

        }

        private void AddChesseAndTomatoButt(object sender, RoutedEventArgs e)
        {

        }

        private void AddHotDogButt(object sender, RoutedEventArgs e)
        {

        }
    }
}
