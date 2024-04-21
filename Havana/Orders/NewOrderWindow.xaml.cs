using System;
using System.Linq;
using System.Windows;
using Havana.Drinks;
using Havana.Snacks;
using Library.Models.Classes;



namespace Havana.Orders
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

        public void BackToOrderWindow(NewOrderWindow newOrderWindow)
        {
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
            OpenWindow(typeof(DrinksItems));
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
