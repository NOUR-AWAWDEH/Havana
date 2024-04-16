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
        }

        private void InseartPhotoButt(object sender, RoutedEventArgs e)
        {
            if (FilePathTexBox.Text != null && PhotoNameTexBox.Text != null && IDTexBox.Text != null)
            {
                DataAccess dataAccess = new DataAccess();
                string filePath = FilePathTexBox.Text;
                string photoName = PhotoNameTexBox.Text;
                int id = Convert.ToInt32(IDTexBox.Text);
                if (SnackPhoto.IsChecked == true)
                {
                    dataAccess.InseartSnackPhoto(filePath, photoName, id);
                    StatusText.Text = "Snack Photo Insearted Sucsessfully!";
                }
                else 
                {
                    dataAccess.InseartDrinkPhoto(filePath, photoName, id);
                    StatusText.Text = "Drink Photo Insearted Sucsessfully!";
                }
                
            }
            else 
            {
                StatusText.Text = "Please Fill the File Path  and the the Photo Name!!!";
            }
            
        }

        private void FilePathButt(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image Files (*.jpg, *.png, *.gif, *.svg, *.ico)|*.jpg;*.png;*.gif;*.svg;*.ico|All Files (*.*)|*.*";

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                FilePathTexBox.Text = filename;
                ImageSource imageSource = new BitmapImage(new Uri(filename));
                InseartImage.Source = imageSource;
            }
        }
    }
}
