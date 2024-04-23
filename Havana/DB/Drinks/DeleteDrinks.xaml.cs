using Havana.Orders;
using Library.Models.Classes;
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
using System.Xml.Linq;

namespace Havana.DB.Drinks
{
    /// <summary>
    /// Interaction logic for DeleteDrinks.xaml
    /// </summary>
    public partial class DeleteDrinks : Window
    {
        DataAccess dataAccess = new DataAccess();

        List<DrinkPhoto> DrinksInfo = null;
        public DeleteDrinks()
        {
            InitializeComponent();
            DrinksInfo = dataAccess.GetDrinksPhotos();
            ShowDrinks(DrinksInfo);
        }

        private void ShowDrinks(List<DrinkPhoto> drinksInfo)
        {
            
            DrinksComboBox.ItemsSource = drinksInfo.Select(drinkPhoto => drinkPhoto.Drink.Name);
            
        }

        private void BackToDataBaseWindowButt(object sender, RoutedEventArgs e)
        {
            DataBaseInfo databaseInfoWindow = Application.Current.Windows.OfType<DataBaseInfo>().FirstOrDefault();

            if (databaseInfoWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                databaseInfoWindow.Visibility = Visibility.Visible;
            }
        }

        private void SearchByNameButt(object sender, RoutedEventArgs e)
        {
            DrinksDataGrid.Items.Clear();
            DrinkImage.Source = null;
            string searchTerm = SearchTextBox.Text.ToLower();

            foreach (DrinkPhoto drinkPhoto in DrinksInfo)
            {
                if (drinkPhoto.Drink.Name.ToLower().StartsWith(searchTerm) ||
                    drinkPhoto.Drink.Cost.ToString().ToLower().StartsWith(searchTerm))
                {
                    DrinksTextBlock.Text = drinkPhoto.Drink.Name;
                    DrinksDataGrid.Items.Add(drinkPhoto.Drink);
                    DrinkImage.Source = drinkPhoto.Image;
                }
            }
        }

        private void DeleteDrinkButt(object sender, RoutedEventArgs e)
        {
            DrinksTextBlock.Text = null;
            if (DrinksComboBox.SelectedItem is string selectedDrinkName)
            {
                Drink selectedDrink = dataAccess.GetDrinkByName(selectedDrinkName);
                if (selectedDrink != null)
                {
                    dataAccess.DeleteDrink(selectedDrink.Id);
                    RefreshWindow();
                    DrinksTextBlock.Text = $"{selectedDrink.Name} Has been Deleted";

                }
            }
            else if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                dataAccess.DeleteDrink(selectedDrink.Id);
                RefreshWindow();
                DrinksTextBlock.Text = $"{selectedDrink.Name} Has been Deleted";

            }
        }

        private void RefreshWindow() 
        {
            
            DeleteDrinks  deleteDrinks = Application.Current.Windows.OfType<DeleteDrinks>().FirstOrDefault();

            if (deleteDrinks != null)
            {
                this.Visibility = Visibility.Hidden;
                deleteDrinks.Visibility = Visibility.Visible;
            }
        }
        private void cb_SelectChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DrinksComboBox.SelectedItem is string selectedDrinkName)
            {
                DrinkPhoto selectedDrinkPhoto = DrinksInfo.FirstOrDefault(drinkPhoto =>
                    drinkPhoto.Drink.Name.Equals(selectedDrinkName));

                if (selectedDrinkPhoto != null)
                {
                    DrinksTextBlock.Text = selectedDrinkPhoto.Drink.Name;
                    DrinksDataGrid.Items.Clear();
                    DrinksDataGrid.Items.Add(selectedDrinkPhoto.Drink);
                    DrinkImage.Source = selectedDrinkPhoto.Image;
                }
            }
        }

        private void DrinksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
                {
                    DrinksComboBox.SelectedItem = selectedDrink.Name;
                    
                }
        }

        private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

    }
}