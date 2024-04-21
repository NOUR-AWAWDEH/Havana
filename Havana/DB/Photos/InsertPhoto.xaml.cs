using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Havana.DB.Photos
{
    /// <summary>
    /// Interaction logic for InseartPhoto.xaml
    /// </summary>
    public partial class InsertPhotos : Window
    {
        public InsertPhotos()
        {
            InitializeComponent();
            ClearStatuseText();
        }

        private void ClearStatuseText() 
        {
            StatusText.Text = null;
        }

        private void FilePathButt(object sender, RoutedEventArgs e)
        {
            
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image Files (*.jpg, *.png, *.gif, *.svg, *.ico)|*.jpg;*.png;*.gif;*.svg;*.ico|All Files (*.*)|*.*";

            
            bool? result = dialog.ShowDialog();
            
            if (result == true)
            {
                string filename = dialog.FileName;
                FilePathTextBox.Text = filename;
                ImageSource imageSource = new BitmapImage(new Uri(filename));
                InsertImage.Source = imageSource;
            }
        }

        private int GetDrinkPhotoID(string str) 
        {
            DataAccess dataAccess = new DataAccess();
            List<Drink> drinks = dataAccess.GetDrinks();
            return FindDrinkID(drinks,str);
        }

        private int GetSnackPhotoID(string str)
        {
            DataAccess dataAccess = new DataAccess();
            List<Snack> snacks = dataAccess.GetSnacks();
            return FindSnackID(snacks,str);
        }

        private int FindDrinkID(List<Drink> drinks, string str) 
        {
            foreach (Drink drink in drinks)
            {
                if (drink.Name == str) 
                {
                    return drink.Id;
                }
            }
            return -1;
        }

        private int FindSnackID(List<Snack> snacks, string str)
        {
            foreach (Snack snack in snacks)
            {
                
                if (snack.Name == str)
                {
                    return snack.Id;
                }
            }
            return -1;
        }


        private void InsertPhotoButt(object sender, RoutedEventArgs e)
        {
            
            if (FilePathTextBox.Text != null && IteamsList.SelectedItem != null)
            {
                DataAccess dataAccess = new DataAccess();
                string filePath = FilePathTextBox.Text;
                
                if (SnacksRadioButton.IsChecked == true)
                {
                    int id = GetSnackPhotoID(IteamsListSelectedItem());
                    dataAccess.InsertSnackPhoto(filePath, id);
                    StatusText.Text = "Snack Photo Insearted Sucsessfully!";
                }
                else
                {
                    int id = GetDrinkPhotoID(IteamsListSelectedItem());
                    dataAccess.InsertDrinkPhoto(filePath, id);
                    StatusText.Text = "Drink Photo Insearted Sucsessfully!";
                }

            }
            else
            {
                StatusText.Text = "Please Fill DATA!!!";
            }
            
        }

        private void RefreshDrinksItems(object sender, RoutedEventArgs e)
        {
           DataAccess dataAccess = new DataAccess();

           List<Drink> drinks = dataAccess.GetDrinks();
           IteamsList.ItemsSource = drinks;
           IteamsList.DisplayMemberPath = "Name";         
        }

        private void RefreshSnackssItems(object sender, RoutedEventArgs e)
        {
            DataAccess dataAccess = new DataAccess();
            List<Snack> snacks = dataAccess.GetSnacks();
            IteamsList.ItemsSource = snacks;
            IteamsList.DisplayMemberPath = "Name";
            
        }

        private string IteamsListSelectedItem()
        {
            if (SnacksRadioButton.IsChecked == true)
            {

                Snack selectedSnack = (Snack)IteamsList.SelectedValue;
                string str = selectedSnack.Name;

                return str;
            }
            else 
            {
                Drink selectedDrink = (Drink)IteamsList.SelectedValue;
                string str = selectedDrink.Name;

                return str;
            }
          
            
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
    }
}
