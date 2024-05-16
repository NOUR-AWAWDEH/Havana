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
using System.Text;

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

        public void SummaryBills(List<Order> orders)
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SummaryReports");
            Directory.CreateDirectory(folderPath);

            try
            {
                string fileName = Path.Combine(folderPath, $"SummaryReport_{orders[0].DateTime:yyyyMMdd_HHmmss}.txt");
                
                StringBuilder sb = new StringBuilder();

                string currencySymbol = "BYN ";
                int itemNameColumnWidth = 70;

                sb.AppendLine("".PadRight(87, '-'));
                sb.AppendLine($"{"".PadRight(24)}Havana".PadRight(45));
                sb.AppendLine($"{"".PadRight(14)}Address 99,99 , City Gomel");
                sb.AppendLine($"{"".PadRight(16)}Phone: +37525-222-2222");
                sb.AppendLine("".PadRight(87, '='));
                sb.AppendLine("\n\n");

                decimal totalRevenue = 0;

                foreach (Order order in orders)
                {
                    sb.AppendLine($"Order ID: {order.Id}");
                    sb.AppendLine($"DateTime: {order.DateTime.ToString("M/d/yyyy h:mm:ss tt")}");
                    sb.AppendLine($"Buyer Name: {(order.BuyerName?.Name ?? "")}");
                    sb.AppendLine("".PadRight(87, '-'));
                    sb.AppendLine("Items".PadRight(itemNameColumnWidth, ' ') + "Price".PadRight(12, ' '));
                    sb.AppendLine("".PadRight(87, '-'));

                    decimal totalCost = 0;

                    Dictionary<int, int> itemDictionary = new Dictionary<int, int>();

                    if (order.DrinksList != null && order.DrinksList.Drinks != null)
                    {
                        foreach (Drink drink in order.DrinksList.Drinks)
                        {
                            if (itemDictionary.ContainsKey(drink.Id))
                            {
                                itemDictionary[drink.Id]++;
                            }
                            else
                            {
                                itemDictionary[drink.Id] = 1;
                            }
                        }
                    }

                    if (order.SnacksList != null && order.SnacksList.Snacks != null)
                    {
                        foreach (Snack snack in order.SnacksList.Snacks)
                        {
                            if (itemDictionary.ContainsKey(snack.Id))
                            {
                                itemDictionary[snack.Id]++;
                            }
                            else
                            {
                                itemDictionary[snack.Id] = 1;
                            }
                        }
                    }

                    foreach (var kvp in itemDictionary)
                    {
                        int itemId = kvp.Key;
                        int itemCount = kvp.Value;

                        if (order.DrinksList != null && order.DrinksList.Drinks != null)
                        {
                            // Find the drink with the matching ID
                            Drink drink = order.DrinksList.Drinks.FirstOrDefault(d => d.Id == itemId);
                            if (drink != null)
                            {
                                string itemLine = $"{(drink.Name?.ToLower() ?? "")}";
                                int remainingSpace = itemNameColumnWidth - itemLine.Length;
                                string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{drink.Cost.ToString("0.0#")}";
                                sb.AppendLine($"{itemLine}{priceLine}{itemCount}");
                                totalCost += drink.Cost * itemCount;
                            }
                        }

                        if (order.SnacksList != null && order.SnacksList.Snacks != null)
                        {
                            // Find the snack with the matching ID
                            Snack snack = order.SnacksList.Snacks.FirstOrDefault(s => s.Id == itemId);
                            if (snack != null)
                            {
                                string itemLine = $"{(snack.Name?.ToLower() ?? "")}";
                                int remainingSpace = itemNameColumnWidth - itemLine.Length;
                                string priceLine = $"{new string(' ', remainingSpace)}{currencySymbol}{snack.Cost.ToString("0.0#")}";
                                sb.AppendLine($"{itemLine}{priceLine}{itemCount}");
                                totalCost += snack.Cost * itemCount;
                            }
                        }
                    }
                    sb.AppendLine("".PadRight(87, '-'));
                    sb.AppendLine($"Drinks Count:".PadRight(itemNameColumnWidth) + $"{(order.DrinksList != null ? order.DrinksList.Count.ToString("0") : "0")}");
                    sb.AppendLine($"Snacks Count:".PadRight(itemNameColumnWidth) + $"{(order.SnacksList != null ? order.SnacksList.Count.ToString("0") : "0")}");
                    sb.AppendLine("".PadRight(87, '-'));
                    sb.AppendLine($"Total:".PadRight(itemNameColumnWidth) + $"{currencySymbol}{totalCost.ToString("0.##")}");
                    sb.AppendLine("".PadRight(87, '='));
                    totalRevenue += totalCost;
                }

                sb.AppendLine();
                sb.AppendLine($"Total Revenue: {currencySymbol}{totalRevenue.ToString("0.##")}");
                sb.AppendLine();
                sb.AppendLine("".PadRight(87, '='));

                File.WriteAllText(fileName, sb.ToString());

                MessageBox.Show("Summary report generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating summary report: {ex.Message}");
            }
        }
    }
}
