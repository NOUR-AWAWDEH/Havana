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

namespace Havana.DB.DrinksType
{
    /// <summary>
    /// Interaction logic for DeleteDrinksType.xaml
    /// </summary>
    public partial class DeleteDrinksType : Window
    {
        public DeleteDrinksType()
        {
            InitializeComponent();
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

        private void DelleteDrinkButt(object sender, RoutedEventArgs e)
        {

        }
    }
}
