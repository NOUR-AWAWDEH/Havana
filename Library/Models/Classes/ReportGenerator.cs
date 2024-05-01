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
                string fileName = Path.Combine(folderPath, $"Bill_{order.OrderDate:yyyyMMdd_HHmmss}.txt");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("--------------------------------------------------------------------------");
                                  
                    sw.WriteLine($"{CenterText("Havana")}");
                    sw.WriteLine($"{CenterText("Address  99,99 , City Gomel")}");
                    sw.WriteLine($"{CenterText("Phone: +37525-222-2222")}");
                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.WriteLine($"Date:        {order.OrderDate}\n");
                    sw.WriteLine($"Buyer Name:  {order.BuyerName}");
                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.WriteLine("Items                                                       Price");
                    sw.WriteLine("--------------------------------------------------------------------------");
                    
                    foreach (var drink in order.Drinks)
                    {
                       
                        string itemLine = $"{drink.Name,-60}"; 
                        string priceLine = $"{currencySymbol}{drink.Cost.ToString("G29")}"; 
                        sw.WriteLine($"{itemLine}{priceLine}");
                    }

                    
                    foreach (var snack in order.Snacks)
                    {
                
                        string itemLine = $"{snack.Name,-60}"; 
                        string priceLine = $"{currencySymbol}{snack.Cost.ToString("G29")}"; 
                        sw.WriteLine($"{itemLine}{priceLine}");
                    }

                  
                    
                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.WriteLine($"Total: {currencySymbol,+57}{order.TotalCost.ToString("G29")}");
                    sw.WriteLine("--------------------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Helper method to center the text within the line
        private string CenterText(string text)
        {
            int totalWidth = 74; // Total width of the line
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
                string fileName = Path.Combine(folderPath, $"Bill_{order.OrderDate:yyyyMMdd_HHmmss}.txt");

                try
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        // Write the order details to the file
                        sw.WriteLine($"Order Date: {order.OrderDate}");
                        sw.WriteLine($"Buyer Name: {order.BuyerName}");
                        sw.WriteLine("Drinks:");
                        foreach (var drink in order.Drinks)
                        {
                            sw.WriteLine($"{drink.Name} - {drink.Cost}");
                        }
                        sw.WriteLine("Snacks:");
                        foreach (var snack in order.Snacks)
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
