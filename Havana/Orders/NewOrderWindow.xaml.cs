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
using System.Data.SqlClient;

namespace Havana.Orders
{
    public partial class NewOrderWindow : Window
    {
        DataAccess dataAccess = new DataAccess();
        ListOfDrinks DrinksList = new ListOfDrinks();
        ListOfSnacks SnacksList = new ListOfSnacks();
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
                    buyer.Id = -1;
                    buyer.Name = name;
                    buyer.Id = dataAccess.InsertBuyer(buyer);
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
            if (itemsAdded is true && BuyerNameTextBox.Text != null)
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
            
            ListOfDrinks listOfDrinks = new ListOfDrinks();
            ListOfSnacks listOfSnacks = new ListOfSnacks();
            List<Drink> drinks = new List<Drink>();
            List<Snack> snacks = new List<Snack>();
            
            newOrder.Id = -1;
            
            foreach (var item in OrderDataGrid.Items)
            {
                if (item is Drink drink)
                {
                    drinks.Add(drink); 
                    listOfDrinks.Count++;
                }
                if (item is Snack snack)
                {
                    
                    snacks.Add(snack);
                    listOfSnacks.Count++;
                }
            }

            if (listOfDrinks != null) 
            {
                listOfDrinks.Id = -1;
            }

            if (listOfSnacks != null)
            { 
                listOfSnacks.Id = -1;
            }

            //Sing Order
            newOrder.Id = -1;
            newOrder.Name = "Table_1";
            newOrder.DateTime = setDateTime;
            newOrder.BuyerName = buyer; 
            listOfDrinks.Drinks = drinks;
            listOfSnacks.Snacks = snacks;
            newOrder.DrinksList = listOfDrinks;
            newOrder.SnacksList = listOfSnacks;
            newOrder.TotalCost = newOrder.CalculateTotalCost();
            ReportGenerator report = new ReportGenerator();
            report.CreateBill(newOrder);

            try
            {
                dataAccess.InsertOrder(newOrder);
                Bill billWindow = new Bill(newOrder);
                billWindow.Show();
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Failed to insert the order into the database: " + ex.Message);
            }
        }



        private void SearchButt(object sender, RoutedEventArgs e)
        {
            DrinksList.Drinks = dataAccess.GetDrinks();
            ItemsComboBox.Items.Clear();

            string searchTerm = SearchTextBox.Text.ToLower();

            foreach (Drink drink in DrinksList.Drinks)
            {
                if (drink.Name.ToLower().StartsWith(searchTerm) || drink.Cost.ToString().ToLower().StartsWith(searchTerm))
                {
                    ItemsComboBox.Items.Add(drink);
                     
                           
                }
            }

            SnacksList.Snacks = dataAccess.GetSnacks();
            foreach (Snack snack in SnacksList.Snacks)
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
           
        }
        
    }
}