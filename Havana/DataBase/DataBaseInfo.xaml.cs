using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Havana.DataBase.Photos;
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
    /// Interaction logic for DataBase.xaml
    /// </summary>
    public partial class DataBaseInfo : Window
    {
        public DataBaseInfo()
        {
            InitializeComponent();
        }

        private void OpenWindow(Type window)
        {

        }

        
        private void InsertPhotosWindowbutt(object sender, RoutedEventArgs e)
        {
            InseartPhoto inseartPhoto = new InseartPhoto();
            this.Visibility = Visibility.Hidden;   
            inseartPhoto.ShowDialog();
        }

        private void DeletePhotoWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void EditDrinksWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void BackToMainWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void EditDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void InsertDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void InsertDrinksWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void EditSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void InsertSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSnacksWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void EditSnacksWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void InsertSnacksWindowButt(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteDrinkWindowButt(object sender, RoutedEventArgs e)
        {

        }
    }
}
