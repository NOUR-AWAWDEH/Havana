using System;
using System.Linq;
using System.Windows;
using Havana.Drinks;
using Havana.Snacks;
using Library.Models.Classes;
using Havana.Main;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Data;

namespace Havana.Orders
{
    public partial class NewOrderWindow : Window
    {
        DataAccess dataAccess = new DataAccess();
        List<Drink> Drinks = new List<Drink>();
        List<Snack> Snacks = new List<Snack>();
        Buyer buyer = new Buyer();
        public bool itemsAdded = false;
        public NewOrderWindow()
        {
            InitializeComponent();
        }

        public void OpenWindow(Type windowType)
        {
            Window window = (Window)Activator.CreateInstance(windowType);
            this.Visibility = Visibility.Visible;
            window.Show();
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
            if (OrderDataGrid.Items.Count == 0) 
            {
                itemsAdded = false;
                ShowOrderButtons();
            }
            
        }

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ItemsComboBox.SelectedValue != null)
            {
                if (ItemsComboBox.SelectedValue is Drink selectedDrink)
                {
                    OrderDataGrid.Items.Add(selectedDrink);
                    itemsAdded = true;
                    ShowOrderButtons();
                }
                else if (ItemsComboBox.SelectedValue is Snack selectedSnack)
                {
                    OrderDataGrid.Items.Add(selectedSnack);
                    itemsAdded = true;
                    ShowOrderButtons();
                }
            }
        }

        //ItemsButt
        private void SnaksItemsButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(SnacksItems));
        }

        private void DrinksItemsButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DrinksItems));
        }

        private void CheckBuyerName() 
        {
            
            try
            {
                string name = BuyerNameTextBox.Text;
                if (name != null && !string.IsNullOrWhiteSpace(name))
                {
                    buyer.Name = name;
                    dataAccess.InsertBuyerName(buyer);
                    OrderTextBlock.Text = name + " Has Been Added!! ";
                }
                else
                {
                    MessageBox.Show("Please Add The Buyer Name First!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void ShowOrderButtons()
        {
            if (itemsAdded is true)
            {
                Order.Visibility = Visibility.Visible;
            }
            else
            {
                Order.Visibility = Visibility.Collapsed; 
            }
        }

        //Buttons       
        private void OrderButt(object sender, RoutedEventArgs e)
        {
            CheckBuyerName();
            Order newOrder = new Order();
            DateTime setDateTime = DateTime.Now;
            
            List<Drink> drinks = new List<Drink>();
            List<Snack> snacks = new List<Snack>();

            foreach (var item in OrderDataGrid.Items)
            {
                if (item is Drink drink)
                {
                    drinks.Add(drink);
                }
                if (item is Snack snack)
                {
                    snacks.Add(snack);
                }
            }

            newOrder.OrderDate = setDateTime;
            newOrder.BuyerName = buyer;
            newOrder.Drinks = drinks;
            newOrder.Snacks = snacks;
            newOrder.TotalCost = newOrder.CalculateTotalCost();
            ReportGenerator report = new ReportGenerator();
            report.CreateBill(newOrder);

            Bill billWindow = new Bill(newOrder);
            billWindow.Show();
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

        private void OrderDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        
    }
}