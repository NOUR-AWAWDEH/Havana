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


namespace Havana.DataBase
{
    /// <summary>
    /// Interaction logic for InseartPhoto.xaml
    /// </summary>
    public partial class InseartPhoto : Window
    {
        public InseartPhoto()
        {
            InitializeComponent();
            ClearStatuseTex();
        }

        private void ClearStatuseTex() 
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
                InseartImage.Source = imageSource;
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


        private void InseartPhotoButt(object sender, RoutedEventArgs e)
        {
            
            if (FilePathTextBox.Text != null && IteamsList.SelectedItem != null)
            {
                DataAccess dataAccess = new DataAccess();
                string filePath = FilePathTextBox.Text;
                
                if (SnacksRadioButton.IsChecked == true)
                {
                    int id = GetSnackPhotoID(IteamsListSelectedItem());
                    dataAccess.InseartSnackPhoto(filePath, id);
                    StatusText.Text = "Snack Photo Insearted Sucsessfully!";
                }
                else
                {
                    int id = GetDrinkPhotoID(IteamsListSelectedItem());
                    dataAccess.InseartDrinkPhoto(filePath, id);
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
            if (IteamsList.SelectedValue != null)
            {

                Snack selectedSnack = (Snack)IteamsList.SelectedValue;
                string str = selectedSnack.Name;
               
                return str;
            }
            return null;
        }

        
    }
}
