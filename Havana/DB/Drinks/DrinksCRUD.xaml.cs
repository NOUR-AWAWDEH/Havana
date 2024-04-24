﻿using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.Windows.Shapes;
using static Havana.HavanaDataSet;

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
            DrinksInfo = dataAccess.GetDrinksPhotos();
            typeOfDrinks = dataAccess.GetDrinkType();
            ShowDrinks(DrinksInfo, typeOfDrinks);
        }

        private void ShowDrinks(List<DrinkPhoto> drinksInfo, List<TypeOfDrink> typeOfDrinks)
        {
            DrinksComboBox.ItemsSource = drinksInfo.Select(drinkPhoto => drinkPhoto.Drink.Name);
            
            TypeOfDrinkComboBox.ItemsSource = typeOfDrinks.Select(typeOfDrink => typeOfDrink.Name);
        }

        private int TypeDrinksSelectedItemID()
        {
            if (TypeOfDrinkComboBox.SelectedItem != null)
            {
                TypeOfDrink selectedType = (TypeOfDrink)TypeOfDrinkComboBox.SelectedItem;
                foreach (TypeOfDrink typeOfDrink in typeOfDrinks)
                {
                    if (selectedType.Name == typeOfDrink.Name)
                    {
                        return typeOfDrink.Id;
                    }
                }
            }

            return -1;
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
            if (DrinksDataGrid.SelectedItem is Drink selectedDrink)
            {
                DrinksComboBox.SelectedItem = selectedDrink.Name;

            }
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
            
        }

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
                    DrinkImage.Source = selectedDrinkPhoto.Image;
                    DrinkTextBlock.Text = selectedDrinkPhoto.Drink.Name;

                    DrinkNameTextBox.Text = selectedDrinkPhoto.Drink.Name;
                    DrinkCostTextBox.Text = selectedDrinkPhoto.Drink.Cost.ToString();
                    DrinkVolumeTextBox.Text = selectedDrinkPhoto.Drink.Volume.ToString();
                }
            }

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
                    DrinkNameTextBox.Text = drinkPhoto.Drink.Name;
                    DrinkCostTextBox.Text = drinkPhoto.Drink.Cost.ToString();
                    DrinkVolumeTextBox.Text = drinkPhoto.Drink.Volume.ToString();

                }
            }
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

        private int GenerateUniqueDrinkId(List<DrinkPhoto> drinkPhotos)
        {
            int maxId = 0;

            foreach (DrinkPhoto drinkPhoto in drinkPhotos)
            {
                if (drinkPhoto.Drink.Id > maxId)
                {
                    maxId = drinkPhoto.Drink.Id;
                }
            }

            int newId = maxId + 1;
            return newId;
        }

        private void AddDrinkButt_Click(object sender, RoutedEventArgs e)
        {
            int id = GenerateUniqueDrinkId(DrinksInfo);
            string nameDrink = DrinkNameTextBox.Text;
            decimal costDrink = Convert.ToDecimal(DrinkCostTextBox.Text);
            double volumeDrink = Convert.ToDouble(DrinkVolumeTextBox.Text);
            int idTypeOFDrink = TypeDrinksSelectedItemID();

            Drink newDrink = new Drink(id, nameDrink, costDrink, volumeDrink, idTypeOFDrink);

            dataAccess.InsertDrink(newDrink);           
            string photoPath = FilePathTextBox.Text;
            dataAccess.InsertDrinkPhoto(photoPath,newDrink.Id);
            DrinkTextBlock.Text = newDrink.Name.ToString() + " has been added!";
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
            
        }

        private void TypeDrinksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeOfDrinkComboBox.SelectedItem != null)
            {
                TypeOfDrink selectedType = (TypeOfDrink)TypeOfDrinkComboBox.SelectedItem;
                DrinkTextBlock.Text = $"{selectedType.Name} has been selected";
            }
        }

    }
}
