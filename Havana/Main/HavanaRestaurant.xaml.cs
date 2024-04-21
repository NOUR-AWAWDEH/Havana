using Havana.Reports;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using Havana.DB;
using Havana.Orders;

namespace Havana.Main
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string connStr;

        public MainWindow()
        {
            InitializeComponent();
            connStr = ConfigurationManager.ConnectionStrings["Havana.Properties.Settings.HavanaConnectionString"].ConnectionString;
        }
        
        private void CloseButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ShowButton(SqlConnection connection) 
        {
            if (connection != null)
            {
                ConnectButton.Visibility = Visibility.Hidden;
                OrderButton.Visibility = Visibility.Visible;
                DataBaseButton.Visibility = Visibility.Visible;
                ReportsButton.Visibility = Visibility.Visible;
            }
        }

        public void ConnectButt(object sender, RoutedEventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                try
                {
                    connection.Open();
                    StatusText.Text = "Connection opened successfully!";

                    // Perform database operations here...
                    ShowButton(connection);

                }
                catch (Exception ex)
                {
                    StatusText.Text = "Error: " + ex.Message;
                }
                finally 
                {
                    //connection.Close();
                    //StatusText.Text = "Connection closed successfully!";
                }
            }
        }

        private void OrderButt(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrder = new NewOrderWindow();
            this.Visibility = Visibility.Hidden;
            newOrder.Show();
        }

        private void DBButt(object sender, RoutedEventArgs e)
        {
            DataBaseInfo newOrder = new DataBaseInfo();
            this.Visibility = Visibility.Hidden;
            newOrder.Show();
        }

        private void ReportsButt(object sender, RoutedEventArgs e)
        {
            ReportsWindow reportsWindow = new ReportsWindow();
            this.Visibility = Visibility.Hidden;
            reportsWindow.Show();
        }
    }
}
