using System;
using System.Linq;
using System.Windows;
using Havana.Drinks;
using Havana.Snacks;
using Library.Models.Classes;
using Havana.Main;
using System.Windows.Input;
using System.Windows.Controls;



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
       
        private void SnaksItemsButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(SnacksItems));
           
        }

        private void DrinksItemsButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DrinksItems));
        }

        private void SearchButt(object sender, RoutedEventArgs e)
        {
            //DrinksDataGrid.Items.Clear();
            //DrinkImage.Source = null;
            //string searchTerm = SearchTextBox.Text.ToLower();

            //foreach (DrinkPhoto drinkPhoto in DrinksInfo)
            //{
            //    if (drinkPhoto.Drink.Name.ToLower().StartsWith(searchTerm) ||
            //        drinkPhoto.Drink.Cost.ToString().ToLower().StartsWith(searchTerm))
            //    {
            //        DrinkTextBlock.Text = drinkPhoto.Drink.Name;
            //        DrinksDataGrid.Items.Add(drinkPhoto.Drink);
            //        DrinkImage.Source = drinkPhoto.Image;
            //        DrinkNameTextBox.Text = drinkPhoto.Drink.Name;
            //        DrinkCostTextBox.Text = drinkPhoto.Drink.Cost.ToString();
            //        DrinkVolumeTextBox.Text = drinkPhoto.Drink.Volume.ToString();

            //    }
            //}
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

        private void DeleteSelectedItemButt_Click(object sender, RoutedEventArgs e)
        {
            // Method implementation
            // Perform actions when the "Delete Selected Item" button is clicked
        }

        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        //private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // Perform any necessary actions when the selection in the OrderDataGrid changes
        //    // For example, you may enable or disable buttons based on the selection status
        //    if (OrderDataGrid.SelectedItem != null)
        //    {
        //        DeleteSelectedItemButt.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        DeleteSelectedItemButt.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void OrderDataGridMouseDoubleClick(object sender, SelectionChangedEventArgs e)
        //{
        //    if (OrderDataGrid.SelectedItem is Drink selectedDrink)
        //    {
        //        int drinkId = selectedDrink.Id;
        //        string drinkName = selectedDrink.Name;
        //        decimal drinkCost = selectedDrink.Cost;
        //        double drinkVolume = selectedDrink.Volume;

        //    }
        //    else if (OrderDataGrid.SelectedItem is Snack selectedSnack)
        //    {

        //        int snackId = selectedSnack.Id;
        //        string snackName = selectedSnack.Name;
        //        decimal snackCost = selectedSnack.Cost;
        //        double snackVolume = selectedSnack.Weigth;
        //    }
        //    else 
        //    {
        //        //Do nothing 
        //}
        //}
    }
}
