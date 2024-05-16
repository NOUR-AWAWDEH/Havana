using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Havana.Orders
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        public Order Order = null;
        public Bill(Order order)
        {
            InitializeComponent();
            ShowTheBill(order);
            Order = order;
        }

        private void ShowTheBill(Order order)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bills");
            Directory.CreateDirectory(folderPath);

            string fileName = System.IO.Path.Combine(folderPath, $"Bill_{order.DateTime:yyyyMMdd_HHmmss}.txt");

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

        private void PrintBill(Order order)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bills");
            Directory.CreateDirectory(folderPath);
            

            string fileName = System.IO.Path.Combine(folderPath, $"Bill_{order.DateTime:yyyyMMdd_HHmmss}.txt");

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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintBill(Order);
        }
    }
}
