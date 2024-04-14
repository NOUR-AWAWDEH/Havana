using Havana.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes; 

namespace Havana
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connStr;

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

        }

        private void ReportsButt(object sender, RoutedEventArgs e)
        {
            ReportsWindow reportsWindow = new ReportsWindow();
            this.Visibility = Visibility.Hidden;
            reportsWindow.Show();
        }
    }
}
