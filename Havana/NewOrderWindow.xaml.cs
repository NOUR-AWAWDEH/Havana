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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Havana.Drinks;
using Havana.Snacks;
using Library.Models.Classes;



namespace Havana
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public NewOrderWindow()
        {
            InitializeComponent();
        }

        private void BackToHavana(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                mainWindow.Visibility = Visibility.Visible;
            }
        }

        public void BackToOrderWindow()
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            if (newOrderWindow != null)
            {
               
                this.Visibility = Visibility.Hidden;
                newOrderWindow.Visibility = Visibility.Visible;
            }
        }

        public void AddSnackItem(int id)
        {
            DataAccess dataAccess = new DataAccess();
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            Snack snack = dataAccess.GetSnack(id);
            newOrderWindow.OrderDataGrid.Items.Add(snack);
            newOrderWindow.Show();
        }

        public void OpenWindow(Type windowType)
        {
            Window window = (Window)Activator.CreateInstance(windowType);
            this.Visibility = Visibility.Visible;
            window.Show();
        }
       
        private void HotSnaksButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(SnacksItems));
           
        }

       

        private void TeaDrinksButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(Tea));
        }

        private void CoffeeDrinksButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(Coffee));
        }

        private void JuicesDrinksButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(Juices));
        }

        private void WaterDrinksButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(Water));
        }

        private void SearchButt(object sender, RoutedEventArgs e)
        {
            
        }

        private void BuyerNameButt(object sender, RoutedEventArgs e)
        {
            DataAccess dataAcces = new DataAccess();
            Buyer buyer = new Buyer();
            string name = BuyerNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                buyer.Name = name;
                dataAcces.InsertBuyerName(buyer);
                BuyerNameTextBox.Text = null;
            }
        }
    }
}
