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

namespace Havana.DB.Snacks
{
    /// <summary>
    /// Interaction logic for SnacksCRUD.xaml
    /// </summary>
    public partial class SnacksCRUD : Window
    {
        DataAccess dataAccess = new DataAccess();
        List<SnackPhoto> SnacksInfo = null;
        List<TypeOfSnack> typeOfSnacks = null;

        public SnacksCRUD()
        {
            InitializeComponent();
            RefreshList();
            ShowSnacks(SnacksInfo, typeOfSnacks);
        }


        //CRUD BUTTONS
        private void AddSnackButt_Click(object sender, RoutedEventArgs e)
        {
            int id = -1;
            string nameSnack = SnackNameTextBox.Text;
            decimal costSnack = Convert.ToDecimal(SnackCostTextBox.Text);
            double weightSnack = Convert.ToDouble(SnackWeightTextBox.Text);
            int idTypeOFSnack = TypeSnacksSelectedItemID();

            Snack newSnack = new Snack(id, nameSnack, costSnack, weightSnack, idTypeOFSnack);

            int newIdSnack = dataAccess.InsertSnack(newSnack);
            string photoPath = FilePathTextBox.Text;
            dataAccess.InsertSnackPhoto(photoPath, newIdSnack);
            SnackTextBlock.Text = newSnack.Name.ToString() + " has been added!";

            RefreshList();
            CleanSnackDataButt_Click(sender, e);

        }

        private void EditSnackButt_Click(object sender, RoutedEventArgs e)
        {

            int snackId = Convert.ToInt32(SnackIdTextBox.Text);
            string snackName = SnackNameTextBox.Text;
            var selectedItem = (TypeOfSnack)TypeOfSnackComboBox.SelectedItem;
            int idTypeOFSnack = selectedItem.Id;
            decimal snackCost = Convert.ToDecimal(SnackCostTextBox.Text);
            double snackWeight = Convert.ToDouble(SnackWeightTextBox.Text);
            Snack updatedSnack = new Snack(snackId, snackName, snackCost, snackWeight, idTypeOFSnack);

            dataAccess.UpdateSnack(updatedSnack);

            SnackTextBlock.Text = $"{updatedSnack.Name} Data IS Updated!! ";

            RefreshList();
            CleanSnackDataButt_Click(sender, e);
        }

        private void DeleteSnackButt_Click(object sender, RoutedEventArgs e)
        {
            SnackTextBlock.Text = null;
            if (SnacksComboBox.SelectedItem is string selectedSnackName)
            {
                Snack selectedSnack = dataAccess.GetSnackByName(selectedSnackName);
                if (selectedSnack != null)
                {
                    dataAccess.DeleteSnack(selectedSnack.Id);
                    
                    SnackTextBlock.Text = $"{selectedSnack.Name} Has been Deleted";

                }
               
            }
            else if (SnacksDataGrid.SelectedItem is Snack selectedSnack)
            {
                dataAccess.DeleteSnack(selectedSnack.Id);
               
                SnackTextBlock.Text = $"{selectedSnack.Name} Has been Deleted";
               
            }

            RefreshList();
            CleanSnackDataButt_Click(sender, e);
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
                SnackImage.Source = imageSource;
            }

            ShowCRUDButtons();
        }

        private void SearchByNameButt(object sender, RoutedEventArgs e)
        {
            SnacksDataGrid.Items.Clear();
            SnackImage.Source = null;
            string searchTerm = SearchTextBox.Text.ToLower();

            foreach (SnackPhoto snackPhoto in SnacksInfo)
            {
                if (snackPhoto.Snack.Name.ToLower().StartsWith(searchTerm) ||
                    snackPhoto.Snack.Cost.ToString().ToLower().StartsWith(searchTerm))
                {
                    SnackTextBlock.Text = snackPhoto.Snack.Name;
                    SnacksDataGrid.Items.Add(snackPhoto.Snack);
                    SnackImage.Source = snackPhoto.Image;
                    SnackNameTextBox.Text = snackPhoto.Snack.Name;
                    SnackCostTextBox.Text = snackPhoto.Snack.Cost.ToString();
                    SnackWeightTextBox.Text = snackPhoto.Snack.Weight.ToString();

                }
            }
            ShowCRUDButtons();
        }

        private void CleanSnackDataButt_Click(object sender, RoutedEventArgs e)
        {

            SnackTextBlock.Text = null;
            SnacksDataGrid.Items.Clear();
            SnackImage.Source = null;
            SnackNameTextBox.Text = null;
            SnackCostTextBox.Text = null;
            SnackWeightTextBox.Text = null;
            FilePathTextBox.Text = null;
            SnacksComboBox.SelectedItem = null;
            SnackIdTextBox.Text = null;
            TypeOfSnackComboBox.SelectedItem = null;
            
        }


        //Data Grind :
        private void SnacksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SnacksDataGrid.SelectedItem is Snack selectedSnack)
            {
                SnacksComboBox.SelectedItem = selectedSnack.Name;

            }

            ShowCRUDButtons();
        }

        private void SnacksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SnacksDataGrid.SelectedItem is Snack selectedSnack)
            {
                int snackId = selectedSnack.Id;
                string snackName = selectedSnack.Name;
                decimal snackCost = selectedSnack.Cost;
                double snackVolume = selectedSnack.Weight;
            }

            ShowCRUDButtons();

        }


        //ComboBox:
        private void SnacksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (SnacksComboBox.SelectedItem is string selectedSnackName)
            {
                
                SnackPhoto selectedSnackPhoto = 
                    SnacksInfo.FirstOrDefault(snackPhoto => snackPhoto.Snack.Name.Equals(selectedSnackName));

                if (selectedSnackPhoto != null)
                {
                    SnackTextBlock.Text = null;
                    SnacksDataGrid.Items.Clear();
                    SnacksDataGrid.Items.Add(selectedSnackPhoto.Snack);
                    var selectedItem = TypeOfSnackComboBox.Items.OfType<TypeOfSnack>().FirstOrDefault(item => item.Id == selectedSnackPhoto.Snack.TypeOfSnakId);
                    TypeOfSnackComboBox.SelectedItem = selectedItem;
                    SnackImage.Source = selectedSnackPhoto.Image;
                    SnackTextBlock.Text = selectedSnackPhoto.Snack.Name;
                    SnackIdTextBox.Text = selectedSnackPhoto.Snack.Id.ToString();
                    SnackNameTextBox.Text = selectedSnackPhoto.Snack.Name;
                    SnackCostTextBox.Text = selectedSnackPhoto.Snack.Cost.ToString();
                    SnackWeightTextBox.Text = selectedSnackPhoto.Snack.Weight.ToString();
                }
            }
            ShowSnacks(SnacksInfo, typeOfSnacks);

        }

        private void TypeSnacksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeOfSnackComboBox.SelectedItem != null)
            {

                var selectedItem = TypeOfSnackComboBox.SelectedItem;
                TypeOfSnack selectedItemType = (TypeOfSnack)selectedItem;
                SnackTextBlock.Text = $"Selected item type: {selectedItemType.Id}";
            }

            ShowCRUDButtons();

        }


        //Implements Methods
        private void ShowSnacks(List<SnackPhoto> SnacksInfo, List<TypeOfSnack> typeOfSnacks)
        {
            SnacksComboBox.ItemsSource = SnacksInfo.Select(snackPhoto => snackPhoto.Snack.Name);

            TypeOfSnackComboBox.ItemsSource = typeOfSnacks;
           
            
            
        }

        private int TypeSnacksSelectedItemID()
        {
            if (TypeOfSnackComboBox.SelectedItem is TypeOfSnack selectedType)
            {
                foreach (TypeOfSnack typeOfSnack in typeOfSnacks)
                {
                    if (selectedType.Id == typeOfSnack.Id)
                    {
                        return typeOfSnack.Id;
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


        private void RefreshList()
        {
           
            SnacksInfo = dataAccess.GetSnacksPhotos();
          
            typeOfSnacks = dataAccess.GetSnacksType();

        }

        private void ShowCRUDButtons()
        {
            if (SnackNameTextBox.Text != null && SnackCostTextBox.Text != null && SnackWeightTextBox.Text != null && TypeOfSnackComboBox.SelectedItem != null)
            {
                AddSnackButt.Visibility = Visibility.Visible;
                DeleteSnackButt.Visibility = Visibility.Visible;
                EditSnackButt.Visibility = Visibility.Visible;
            }
        }

       
    }
}
