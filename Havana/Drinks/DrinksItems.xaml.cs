using System.Linq;
using System.Windows;
using Havana.Orders;

namespace Havana.Drinks
{
    /// <summary>
    /// Interaction logic for Tea.xaml
    /// </summary>
    public partial class DrinksItems : Window
    {
        public DrinksItems()
        {
            InitializeComponent();
        }

        private void BackToOrder(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.BackToOrderWindow(newOrderWindow);
            this.Close();
        }
    }
}
