using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Library.Models.Classes 
{
    public class ReportGenerator
    {
        public List<Order> OrderList { get; set; }

        public ReportGenerator() { }    
        public ReportGenerator(List<Order> orderList)
        {
            this.OrderList = orderList;

        }

        public void CreateBill(Order order)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bills");
            Directory.CreateDirectory(folderPath);

            string currencySymbol = "BYN ";
            try
            {
                string fileName = Path.Combine(folderPath, $"Bill_{order.DateTime:yyyyMMdd_HHmmss}.txt");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    // Assuming sw is the output stream or writer

                    // Calculate the maximum width of the snack names
                    int maxWidth = 60;
                    

                    sw.WriteLine("".PadRight(80, '-'));
                    sw.WriteLine($"{"".PadRight(34)}Havana");
                    sw.WriteLine($"{"".PadRight(24)}Address 99,99 , City Gomel");
                    sw.WriteLine($"{"".PadRight(26)}Phone: +37525-222-2222");
                    sw.WriteLine("".PadRight(80, '-'));

                    sw.WriteLine($"Date: {DateTime.Now.ToString("M/d/yyyy h:mm:ss tt")}");
                    sw.WriteLine($"Buyer Name: {order.BuyerName.Name}");
                    sw.WriteLine("".PadRight(80, '-'));

                    sw.WriteLine("Items".PadRight(60, ' ') + "Price".PadRight(12,' ') + "Count");
                    sw.WriteLine("".PadRight(80, '-'));

                    if (order.DrinksList.Drinks != null && order.DrinksList.Drinks.Any())
                    {
                        foreach (Drink drink in order.DrinksList.Drinks)
                        {
                            string itemLine = $"{drink.Name.ToLower()}";
                            int remainingSpace = maxWidth - itemLine.Length;
                            string priceLine = $"{currencySymbol}{drink.Cost.ToString("G29")}".PadRight(12, ' ');
                            string countOfItems = $"{order.DrinksList.Count}";
                            sw.WriteLine($"{itemLine}{new string(' ', remainingSpace)}{priceLine}{new string(' ', 8)}{countOfItems}");
                        }
                    }

                    if (order.SnacksList.Snacks != null && order.SnacksList.Snacks.Any())
                    {
                        foreach (Snack snack in order.SnacksList.Snacks)
                        {
                            string itemLine = $"{snack.Name.ToLower()}";
                            int remainingSpace = maxWidth - itemLine.Length;
                            string priceLine = $"{currencySymbol}{snack.Cost.ToString("G29")}".PadRight(12);
                            string countOfItems = $"{order.SnacksList.Count}";
                            sw.WriteLine($"{itemLine}{new string(' ', remainingSpace)}{priceLine}{new string(' ', 8)}{countOfItems}");
                        }
                    }

                    sw.WriteLine("".PadRight(80, '-'));
                    sw.WriteLine("Total:".PadRight(maxWidth) + $"{currencySymbol}{order.TotalCost.ToString("0.##")}");
                    sw.WriteLine("".PadRight(80, '-'));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string CenterText(string text)
        {
            int totalWidth = 82; // Total width of the line
            int textWidth = text.Length;
            int leftIndent = (totalWidth - textWidth) / 2;
            string centeredText = text.PadLeft(leftIndent + textWidth).PadRight(totalWidth);
            return centeredText;
        }

        
        public void SummaryReport(List<Order> orderList)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bills");
            Directory.CreateDirectory(folderPath);

            foreach (var order in orderList)
            {
                string fileName = Path.Combine(folderPath, $"Bill_{order.DateTime:yyyyMMdd_HHmmss}.txt");

                try
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        // Write the order details to the file
                        sw.WriteLine($"Order Date: {order.DateTime}");
                        sw.WriteLine($"Buyer Name: {order.BuyerName}");
                        sw.WriteLine("Drinks:");
                        foreach (var drink in order.DrinksList.Drinks)
                        {
                            sw.WriteLine($"{drink.Name} - {drink.Cost}");
                        }
                        sw.WriteLine("Snacks:");
                        foreach (var snack in order.SnacksList.Snacks)
                        {
                            sw.WriteLine($"{snack.Name} - {snack.Cost}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
