using Havana.DB.Photos;
using System.Linq;
using System.Windows;

namespace Havana.DB
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
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                mainWindow.Visibility = Visibility.Visible;
            }
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
