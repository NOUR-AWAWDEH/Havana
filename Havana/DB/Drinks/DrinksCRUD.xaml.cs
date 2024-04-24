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

namespace Havana.DB.Drinks
{
    /// <summary>
    /// Interaction logic for EditDrinks.xaml
    /// </summary>
    public partial class DrinksCRUD : Window
    {

        DataAccess dataAccess = new DataAccess();
        List<DrinkPhoto> DrinksInfo = null; 
        public DrinksCRUD()
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

        private void EditDrinkButt_Click(object sender, RoutedEventArgs e)
        {

        }


        private void FilePathButt_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image Files (*.jpg, *.png, *.gif, *.svg, *.ico,*.jpeg)|*.jpg;*.png;*.gif;*.svg;*.ico|All Files (*.*)|*.*";


            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                FilePathTextBox.Text = filename;
                ImageSource imageSource = new BitmapImage(new Uri(filename));
                DrinkImage.Source = imageSource;
            }
        }

        private void DrinksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DrinksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DrinksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
        }

        private void DeleteDrinkButt_Click(object sender, RoutedEventArgs e)
        {
            DrinkTextBlock.Text = null;
            if (DrinksComboBox.SelectedItem is string selectedDrinkName)
            {
                Drink selectedDrink = dataAccess.GetDrinkByName(selectedDrinkName);
                if (selectedDrink != null)
                {
                    dataAccess.DeleteDrink(selectedDrink.Id);
                    RefreshWindow();
                    DrinkTextBlock.Text = $"{selectedDrink.Name} Has been Deleted";

                }
            }
            else if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                dataAccess.DeleteDrink(selectedDrink.Id);
                RefreshWindow();
                DrinkTextBlock.Text = $"{selectedDrink.Name} Has been Deleted";

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
                    DrinkTextBlock.Text = drinkPhoto.Drink.Name;
                    DrinksDataGrid.Items.Add(drinkPhoto.Drink);
                    DrinkImage.Source = drinkPhoto.Image;
                }
            }
        }
        
        private void RefreshWindow()
        {

            DrinksCRUD drinksCRUD = Application.Current.Windows.OfType<DrinksCRUD>().FirstOrDefault();

            if (drinksCRUD != null)
            {
                this.Visibility = Visibility.Hidden;
                drinksCRUD.Visibility = Visibility.Visible;
            }
        }

        private void AddDrinkButt_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void cb_SelectChanged(object sender, SelectionChangedEventArgs e)
        //{

        //    if (DrinksComboBox.SelectedItem is string selectedDrinkName)
        //    {
        //        DrinkPhoto selectedDrinkPhoto = DrinksInfo.FirstOrDefault(drinkPhoto =>
        //            drinkPhoto.Drink.Name.Equals(selectedDrinkName));

        //        if (selectedDrinkPhoto != null)
        //        {
        //            DrinksTextBlock.Text = selectedDrinkPhoto.Drink.Name;
        //            DrinksDataGrid.Items.Clear();
        //            DrinksDataGrid.Items.Add(selectedDrinkPhoto.Drink);
        //            DrinkImage.Source = selectedDrinkPhoto.Image;
        //        }
        //    }
        //}

        //private void DrinksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
        //    {
        //        DrinksComboBox.SelectedItem = selectedDrink.Name;

        //    }
        //}

        //private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}

    }
}
