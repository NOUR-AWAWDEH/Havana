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
            int itemNameColumnWidth = 70;

            try
            {
                string fileName = Path.Combine(folderPath, $"Bill_{order.DateTime:yyyyMMdd_HHmmss}.txt");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine($"{"".PadRight(24)}Havana".PadRight(45));

                    sw.WriteLine($"{"".PadRight(14)}Address 99,99 , City Gomel");
                    sw.WriteLine($"{"".PadRight(16)}Phone: +37525-222-2222");
                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine($"DateTime :  {order.DateTime.ToString("M/d/yyyy h:mm:ss tt")}");
                    sw.WriteLine($"Buyer Name: {(order.BuyerName?.Name ?? "")}");
                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine("Items".PadRight(itemNameColumnWidth, ' ') + "Price".PadRight(12, ' ') + "Count");
                    sw.WriteLine("".PadRight(87, '-'));

                    if (order.DrinksList != null)
                    {
                        foreach (Drink drink in order.DrinksList.Drinks)
                        {
                            string itemLine = $"{(drink.Name?.ToLower() ?? "")}";
                            int remainingSpace = itemNameColumnWidth - itemLine.Length;
                            string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{drink.Cost.ToString("0.0#")}";
                            string countOfItems = $"".PadRight(5, ' ') + $"{drink.Count}";
                            sw.WriteLine($"{itemLine}{priceLine}{countOfItems}");
                        }
                    }

                    if (order.SnacksList != null)
                    {
                        foreach (Snack snack in order.SnacksList.Snacks)
                        {
                            string itemLine = $"{(snack.Name?.ToLower() ?? "")}";
                            int remainingSpace = itemNameColumnWidth - itemLine.Length;
                            string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{snack.Cost.ToString("0.0#")}";
                            string countOfItems = $"".PadRight(5, ' ') + $"{snack.Count}";
                            sw.WriteLine($"{itemLine}{priceLine}{countOfItems}");
                        }
                    }

                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine($"Total:".PadRight(itemNameColumnWidth) + $"{currencySymbol}{order.TotalCost.ToString("0.##")}");
                    sw.WriteLine("".PadRight(87, '-'));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SummeryBills(List<Order> orders)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SummaryReports");
            Directory.CreateDirectory(folderPath);

            try
            {
                string fileName = Path.Combine(folderPath, $"SummaryReport_{orders[0].DateTime:yyyyMMdd_HHmmss}.txt");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string currencySymbol = "BYN ";
                    int itemNameColumnWidth = 70;

                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine($"{"".PadRight(24)}Havana".PadRight(45));
                    sw.WriteLine($"{"".PadRight(14)}Address 99,99 , City Gomel");
                    sw.WriteLine($"{"".PadRight(16)}Phone: +37525-222-2222");
                    sw.WriteLine("".PadRight(87, '='));
                    sw.WriteLine("\n\n");

                    decimal TotalRevenue = 0;
                    foreach (Order order in orders)
                    {
                        sw.WriteLine($"Order ID: {order.Id}");
                        sw.WriteLine($"DateTime: {order.DateTime.ToString("M/d/yyyy h:mm:ss tt")}");
                        sw.WriteLine($"Buyer Name: {(order.BuyerName?.Name ?? "")}");
                        sw.WriteLine("".PadRight(87, '-'));
                        sw.WriteLine("Items".PadRight(itemNameColumnWidth, ' ') + "Price".PadRight(12, ' ') + "Count");
                        sw.WriteLine("".PadRight(87, '-'));


                        decimal TotalCost = 0;
                        
                      
                        if (order.DrinksList.Drinks != null && order.DrinksList.Drinks != null )
                        {
                            foreach (Drink drink in order.DrinksList.Drinks)
                            {
                                string itemLine = $"{(drink.Name?.ToLower() ?? "")}";
                                int remainingSpace = itemNameColumnWidth - itemLine.Length;
                                string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{drink.Cost.ToString("0.0#")}";
                                string countOfItems = $"".PadRight(5, ' ') + $"{order.DrinksList.Count}";
                                sw.WriteLine($"{itemLine}{priceLine}{countOfItems}");
                                TotalCost += order.DrinksList.Count * drink.Cost;
                                TotalRevenue += TotalCost;

                            }
                        }

                        if (order.SnacksList != null && order.SnacksList.Snacks != null)
                        {
                           
                       
                            foreach (Snack snack in order.SnacksList.Snacks)
                            {
                                string itemLine = $"{(snack.Name?.ToLower() ?? "")}";
                                int remainingSpace = itemNameColumnWidth - itemLine.Length;
                                string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{snack.Cost.ToString("0.0#")}";
                                string countOfItems = $"".PadRight(5, ' ') + $"{order.SnacksList.Count}";
                                sw.WriteLine($"{itemLine}{priceLine}{countOfItems}");
                                TotalCost += order.SnacksList.Count * snack.Cost;
                                TotalRevenue += TotalCost;
                            }
                        }

                        sw.WriteLine("".PadRight(87, '-'));
                        sw.WriteLine($"Total:".PadRight(itemNameColumnWidth) + $"{currencySymbol}{TotalCost.ToString("0.##")}");
                        sw.WriteLine("".PadRight(87, '='));
                    }
                    sw.WriteLine();
                    sw.WriteLine($"Total Revenu = {TotalRevenue}");
                    sw.WriteLine();
                    sw.WriteLine("".PadRight(87, '='));
                }

                MessageBox.Show("Summary report generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating summary report: {ex.Message}");
            }
        }
    }
}
