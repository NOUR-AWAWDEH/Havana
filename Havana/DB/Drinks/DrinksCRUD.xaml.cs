using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Havana.DB.Drinks
{
    /// <summary>
    /// Interaction logic for EditDrinks.xaml
    /// </summary>
    public partial class DrinksCRUD : Window
    {

        DataAccess dataAccess = new DataAccess();
        List<DrinkPhoto> DrinksInfo = null;
        List<TypeOfDrink> typeOfDrinks = null;

        public DrinksCRUD()
        {
            InitializeComponent();
            RefreshList();
            ShowDrinks(DrinksInfo, typeOfDrinks);
        }


        //CRUD BUTTONS
        private void AddDrinkButt_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            string nameDrink = DrinkNameTextBox.Text;
            decimal costDrink = Convert.ToDecimal(DrinkCostTextBox.Text);
            double volumeDrink = Convert.ToDouble(DrinkVolumeTextBox.Text);
            int idTypeOFDrink = TypeDrinksSelectedItemID();

            Drink newDrink = new Drink(id, nameDrink, costDrink, volumeDrink, idTypeOFDrink);

            int newIdDrink = dataAccess.InsertDrink(newDrink);
            string photoPath = FilePathTextBox.Text;
            dataAccess.InsertDrinkPhoto(photoPath, newIdDrink);
            DrinkTextBlock.Text = newDrink.Name.ToString() + " has been added!";

            RefreshList();
        }

        private void EditDrinkButt_Click(object sender, RoutedEventArgs e)
        {

            int drinkId = Convert.ToInt32(DrinkIdTextBox.Text);
            string drinkName = DrinkNameTextBox.Text;
            var selectedItem = (TypeOfDrink)TypeOfDrinkComboBox.SelectedItem;
            int idTypeOFDrink = selectedItem.Id;
            decimal drinkCost = Convert.ToDecimal(DrinkCostTextBox.Text);
            double drinkVolume = Convert.ToDouble(DrinkVolumeTextBox.Text);
            Drink updatedDrink = new Drink(drinkId, drinkName, drinkCost, drinkVolume, idTypeOFDrink);

            dataAccess.UpdateDrink(updatedDrink);
            DrinkTextBlock.Text = $"{updatedDrink.Name} Data IS Updated!! ";

            RefreshList();
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
                RefreshList();
            }
            else if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                dataAccess.DeleteDrink(selectedDrink.Id);
                RefreshWindow();
                DrinkTextBlock.Text = $"{selectedDrink.Name} Has been Deleted";

            }
        }


        //Other Buttons
        private void BackToDataBaseWindowButt(object sender, RoutedEventArgs e)
        {
            DataBaseInfo databaseInfoWindow = Application.Current.Windows.OfType<DataBaseInfo>().FirstOrDefault();

            if (databaseInfoWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                databaseInfoWindow.Visibility = Visibility.Visible;
            }
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
            ShowCRUDButtons();
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
                    DrinkNameTextBox.Text = drinkPhoto.Drink.Name;
                    DrinkCostTextBox.Text = drinkPhoto.Drink.Cost.ToString();
                    DrinkVolumeTextBox.Text = drinkPhoto.Drink.Volume.ToString();

                }
            }
            ShowCRUDButtons();
        }
       
        private void CleanDrinkDataButt_Click(object sender, RoutedEventArgs e)
        {

            DrinkTextBlock.Text = null;
            DrinksDataGrid.Items.Clear();
            DrinkImage.Source = null;
            DrinkNameTextBox.Text = null;
            DrinkCostTextBox.Text = null;
            DrinkVolumeTextBox.Text = null;
            FilePathTextBox.Text = null;
            DrinksComboBox.SelectedItem = null;
            DrinkIdTextBox.Text = null;
            TypeOfDrinkComboBox.SelectedItem = null;
            RefreshList();
        }


        //Data Grind :
        private void DrinksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                DrinksComboBox.SelectedItem = selectedDrink.Name;

            }
            ShowCRUDButtons();
        }

        private void DrinksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                int drinkId = selectedDrink.Id;  
                string drinkName = selectedDrink.Name;
                decimal drinkCost = selectedDrink.Cost;
                double drinkVolume = selectedDrink.Volume; 
                   
            }
            ShowCRUDButtons();

        }


        //ComboBox:
        private void DrinksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DrinksComboBox.SelectedItem is string selectedDrinkName)
            {
                DrinkPhoto selectedDrinkPhoto = DrinksInfo.FirstOrDefault(drinkPhoto =>
                    drinkPhoto.Drink.Name.Equals(selectedDrinkName));

                if (selectedDrinkPhoto != null)
                {
                    DrinkTextBlock.Text = null;
                    DrinksDataGrid.Items.Clear();
                    DrinksDataGrid.Items.Add(selectedDrinkPhoto.Drink);
                    var selectedItem = TypeOfDrinkComboBox.Items.OfType<TypeOfDrink>().FirstOrDefault(item => item.Id == selectedDrinkPhoto.Drink.TypeOfDrinkId);
                    TypeOfDrinkComboBox.SelectedItem = selectedItem;
                    DrinkImage.Source = selectedDrinkPhoto.Image;
                    DrinkTextBlock.Text = selectedDrinkPhoto.Drink.Name;
                    DrinkIdTextBox.Text = selectedDrinkPhoto.Drink.Id.ToString();
                    DrinkNameTextBox.Text = selectedDrinkPhoto.Drink.Name;
                    DrinkCostTextBox.Text = selectedDrinkPhoto.Drink.Cost.ToString();
                    DrinkVolumeTextBox.Text = selectedDrinkPhoto.Drink.Volume.ToString();
                }
            }
            

        }

        private void TypeDrinksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TypeOfDrinkComboBox.SelectedItem != null) 
            { 

                var selectedItem = TypeOfDrinkComboBox.SelectedItem;
                TypeOfDrink selectedItemType = (TypeOfDrink)selectedItem;
                DrinkTextBlock.Text = $"Selected item type: {selectedItemType.Id}";
            }
            ShowCRUDButtons();
        }


        //Implements Methods
        private void ShowDrinks(List<DrinkPhoto> drinksInfo, List<TypeOfDrink> typeOfDrinks)
        {
            DrinksComboBox.ItemsSource = drinksInfo.Select(drinkPhoto => drinkPhoto.Drink.Name);

            TypeOfDrinkComboBox.ItemsSource = typeOfDrinks;
            ShowCRUDButtons();

        }

        private int TypeDrinksSelectedItemID()
        {
            if (TypeOfDrinkComboBox.SelectedItem is TypeOfDrink selectedType)
            {
                foreach (TypeOfDrink typeOfDrink in typeOfDrinks)
                {
                    if (selectedType.Id == typeOfDrink.Id)
                    {
                        return typeOfDrink.Id;
                    }
                }
            }
            ShowCRUDButtons();
            return -1;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
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

        private void RefreshList()
        {
            DrinksInfo = dataAccess.GetDrinksPhotos();
            typeOfDrinks = dataAccess.GetDrinkType();
        }

        private void ShowCRUDButtons()
        {
            if (DrinkNameTextBox.Text != null && DrinkCostTextBox.Text != null && DrinkVolumeTextBox.Text != null && DrinkImage.Source != null && TypeOfDrinkComboBox.SelectedItem != null)
            {
                AddDrinkButt.Visibility = Visibility.Visible;
                DeleteDrinkButt.Visibility = Visibility.Visible;
                EditDrinkButt.Visibility = Visibility.Visible;
            }
        }


    }
}
