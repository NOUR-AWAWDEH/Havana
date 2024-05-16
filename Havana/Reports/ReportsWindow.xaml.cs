using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Havana.Main;
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
            ShowDatesInComboBox();
        }

        private void ShowDatesInComboBox()
        {
            DataAccess dataAccess = new DataAccess();
            List<string> dateTimeList = dataAccess.GetOrdersDates();

            foreach (string dateTime in dateTimeList)
            {
                FromDateTime.Items.Add(dateTime);
                ToDateTime.Items.Add(dateTime);
            }
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
                string fromDateTime = FromDateTime.SelectedItem.ToString();
                string toDateTime = ToDateTime.SelectedItem.ToString();

                if(FromDateTime.SelectedItem != null && ToDateTime.SelectedItem != null) 
                {
                
                    DataAccess dataAccess = new DataAccess();
                    List<Order> orders = dataAccess.GetAllOrders(fromDateTime, toDateTime);

                    ReportGenerator report = new ReportGenerator();
                    report.SummaryBills(orders);
                                      
                    AllOrders allOrders = new AllOrders(orders);
                    allOrders.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    allOrders.Show();
                }
                else
                {
                    MessageBox.Show("Please Add From TO Date Time : ","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
