using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Havana.Main;
using Havana.Orders;
using Library.Models.Classes;

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

        private void BringAllOrders_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DataAccess dataAccess = new DataAccess();
                List<Order> orders = dataAccess.GetAllOrders();
                ReportGenerator report = new ReportGenerator();
                report.SummeryBills(orders);

                AllOrders allOrders = new AllOrders(orders);
                allOrders.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                allOrders.Show();
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Failed to Bring the orders From the  the database: " + ex.Message);
            }
        }
    }
}
