using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Havana.Orders
{
    /// <summary>
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        
        public Bill(Order order)
        {
            InitializeComponent();
            PrintBill(order);
        }

        public void PrintBill(Order order)
        {
            string folderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bills");
            Directory.CreateDirectory(folderPath);

            string fileName = System.IO.Path.Combine(folderPath, $"Bill_{order.OrderDate:yyyyMMdd_HHmmss}.txt");

            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string content = sr.ReadToEnd();
                    billTexBlok.Text = content;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
