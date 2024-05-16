using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Brushes = System.Drawing.Brushes;

namespace Havana.Reports
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class AllOrders : Window
    {
        public List<Order> Orders = null;
        public AllOrders(List<Order> orders)
        {
            InitializeComponent();
            ShowTheBills(orders);
            Orders = orders;
        }

        private void ShowTheBills(List<Order> orders)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SummaryReports");
            Directory.CreateDirectory(folderPath);

            string fileName = System.IO.Path.Combine(folderPath, $"SummaryReport_{orders[0].DateTime:yyyyMMdd_HHmmss}.txt");

            try
            {
                string content = File.ReadAllText(fileName);
                BillTextBlock.Text = content;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PrintFileContent(List<Order> orders)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SummaryReports");
            string fileName = System.IO.Path.Combine(folderPath, $"SummaryReport_{orders[0].DateTime:yyyyMMdd_HHmmss}.txt");

            try
            {
                if (File.Exists(fileName))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Verb = "Print"
                    };

                    Process.Start(psi);
                    MessageBox.Show("Printed successfully.");
                }
                else
                {
                    MessageBox.Show("The summary report file does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFileContent(Orders);
        }
    }
}
