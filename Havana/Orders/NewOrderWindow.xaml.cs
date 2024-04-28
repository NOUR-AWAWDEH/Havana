using System;
using System.Linq;
using System.Windows;
using Havana.Drinks;
using Havana.Snacks;
using Library.Models.Classes;
using Havana.Main;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Havana.Orders
{
    public partial class NewOrderWindow : Window
    {
        DataAccess dataAccess = new DataAccess();
        List<Drink> Drinks = new List<Drink>();
        List<Snack> Snacks = new List<Snack>();
        Buyer buyer = new Buyer();

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
            Snack snack = dataAccess.GetSnack(id);
            OrderDataGrid.Items.Add(snack);
            this.Show();
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
            Drinks = dataAccess.GetDrinks();
            ItemsComboBox.Items.Clear();

            string searchTerm = SearchTextBox.Text.ToLower();

            foreach (Drink drink in Drinks)
            {
                if (drink.Name.ToLower().StartsWith(searchTerm) || drink.Cost.ToString().ToLower().StartsWith(searchTerm))
                {
                    ItemsComboBox.Items.Add(drink);
                     
                           
                }
            }

            Snacks = dataAccess.GetSnacks();
            foreach (Snack snack in Snacks)
            {
                if (snack.Name.ToLower().StartsWith(searchTerm) || snack.Cost.ToString().ToLower().StartsWith(searchTerm))
                {
                    ItemsComboBox.Items.Add(snack);                  
                }
            }
        }

        private void DeleteSelectedItemButt_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem != null)
            {
                OrderDataGrid.Items.Remove(OrderDataGrid.SelectedItem);
            }
        }

        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteSelectedItemButt.Visibility = OrderDataGrid.SelectedItem != null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (ItemsComboBox.SelectedValue != null)
            {
                if (ItemsComboBox.SelectedValue is Drink selectedDrink)
                {
                    OrderDataGrid.Items.Add(selectedDrink);
                }
                else if (ItemsComboBox.SelectedValue is Snack selectedSnack)
                {
                    OrderDataGrid.Items.Add(selectedSnack);
                }
            }
        }

        public void BuyerNameButt(object sender, RoutedEventArgs e)
        {
            DataAccess dataAcces = new DataAccess();
            
            string name = BuyerNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                buyer.Name = name;
                dataAcces.InsertBuyerName(buyer);
                NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
                newOrderWindow.Show();

            }
        }
    }
}