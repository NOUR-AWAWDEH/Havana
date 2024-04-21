using Havana.DB.Photos;
using System.Linq;
using System.Windows;
using Havana.Main;
using Havana.DB.DrinksType;
using System;
using Havana.DB.Drinks;
using Havana.DB.Snacks;
using Havana.DB.SnacksType;

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

        public void OpenWindow(Type windowType)
        {
            Window window = (Window)Activator.CreateInstance(windowType);
            this.Visibility = Visibility.Visible;
            window.Show();
        }

       
        // Photos :
        private void InsertPhotosWindowbutt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(InsertPhotos));
        }

        private void EditPhotosWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditPhotos));
        }

        private void DeletePhotosWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DeletePhotos));
        }



        // Snacks :
        private void InsertSnacksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(InsertSnacks));
        }

        private void EditSnacksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditSnacks));
        }

        private void DeleteSnacksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DeleteSnacks));
        }



        // Snacks Type :
        private void InsertSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(InsertSnacksType));
        }

        private void EditSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditSnacksType));
        }

        private void DeleteSnacksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DeleteSnacksType));
        }



        // Drinks :
        private void InsertDrinksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(InsertDrinks));
        }

        private void EditDrinksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditDrinks));
        }

        private void DeleteDrinksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DeleteDrinks));
        }


        // Drinks Type :
        private void InsertDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(InsertDrinksType));
        }

        private void EditDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditDrinksType));
        }

        private void DeleteDrinksTypeWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DeleteDrinksType));
        }

        //To  Main :
        private void BackToMainWindowButt(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                mainWindow.Visibility = Visibility.Visible;
            }
        }

    }
}
