using System.Linq;
using System.Windows;

namespace Havana.Reports
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
        }

        private void BackToHavana(object sender, RoutedEventArgs e)
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
