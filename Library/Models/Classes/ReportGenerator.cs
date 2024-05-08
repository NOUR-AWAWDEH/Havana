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
                    sw.WriteLine($"Buyer Name: {order.BuyerName.Name}");
                    sw.WriteLine("".PadRight(87, '-'));
                    sw.WriteLine("Items".PadRight(itemNameColumnWidth, ' ') + "Price".PadRight(12, ' ') + "Count");
                    sw.WriteLine("".PadRight(87, '-'));
                    if (order.DrinksList.Drinks != null && order.DrinksList.Drinks.Any())
                    {

                        foreach (Drink drink in order.DrinksList.Drinks)
                        {
                            string itemLine = $"{drink.Name.ToLower()}";
                            int remainingSpace = itemNameColumnWidth - itemLine.Length;
                            string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{drink.Cost.ToString("0.0#")}";
                            string countOfItems = $"".PadRight(5, ' ') + $"{drink.Count}";
                            sw.WriteLine($"{itemLine}{priceLine}{countOfItems}");
                        }

                    }

                    if (order.SnacksList.Snacks != null && order.SnacksList.Snacks.Any())
                    {
                        foreach (Snack snack in order.SnacksList.Snacks)
                        {
                            string itemLine = $"{snack.Name.ToLower()}";
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
                string fileName = Path.Combine(folderPath, $"SummaryReport_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Summary Report");
                    sw.WriteLine("===============");
                    sw.WriteLine();

                    decimal totalCost = 0;

                    foreach (Order order in orders)
                    {
                        sw.WriteLine($"Order ID: {order.Id}");
                        sw.WriteLine($"Buyer Name: {order.BuyerName.Name}");
                        sw.WriteLine($"Date: {order.DateTime.ToString("M/d/yyyy h:mm:ss tt")}");
                        sw.WriteLine();

                        decimal orderCost = 0;

                        if (order.DrinksList.Drinks != null && order.DrinksList.Drinks.Any())
                        {
                            sw.WriteLine("Drinks:");
                            foreach (Drink drink in order.DrinksList.Drinks)
                            {
                                decimal drinkTotalCost = drink.Cost * drink.Count;
                                orderCost += drinkTotalCost;
                                sw.WriteLine($"- {drink.Name}: {drink.Count} x {drink.Cost.ToString("0.0#")} = {drinkTotalCost.ToString("0.####")}");
                            }
                            sw.WriteLine();
                        }

                        if (order.SnacksList.Snacks != null && order.SnacksList.Snacks.Any())
                        {
                            sw.WriteLine("Snacks:");
                            foreach (Snack snack in order.SnacksList.Snacks)
                            {
                                decimal snackTotalCost = snack.Cost * snack.Count;
                                orderCost += snackTotalCost;
                                sw.WriteLine($"- {snack.Name}: {snack.Count} x {snack.Cost.ToString("0.0#")} = {snackTotalCost.ToString("0.####")}");
                            }
                            sw.WriteLine();
                        }

                        sw.WriteLine($"Order Total Cost: {orderCost.ToString("0.####")}");
                        sw.WriteLine();
                        sw.WriteLine("=======================================");
                        sw.WriteLine();

                        totalCost += orderCost;
                    }

                    sw.WriteLine($"Summary Total Cost: {totalCost.ToString("0.####")}");
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
