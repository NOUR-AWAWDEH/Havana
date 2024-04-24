
using System.Linq;
using System.Windows;
using Havana.Main;
using System;
using Havana.DB.Drinks;
using Havana.DB.Snacks;

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

       
        // Snacks :
  
        private void EditSnacksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(EditSnacks));
        }

        // Drinks :
        
        private void EditDrinksWindowButt(object sender, RoutedEventArgs e)
        {
            OpenWindow(typeof(DrinksCRUD));
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
