using Library.Models.Interfaces.Drinks;
using System;
using System.Collections.Generic;

namespace Library.Models.Classes
{
    public class Order
    {
        public DateTime OrderDate { get; set; }

        public Buyer BuyerName { get; set; }

        public List<Drink> Drinks { get; set; }

        public List<Snack> Snacks { get; set; }

        public decimal TotalCost {get;set;}

        public Order() { }

        public Order(DateTime orderDate, Buyer buyerName, List<Drink> drinks, List<Snack> snacks, decimal totalCost)
        {
            OrderDate = orderDate;
            BuyerName = buyerName;
            Drinks = drinks;
            Snacks = snacks;
            TotalCost = totalCost;
        }

        public decimal CalculateTotalCost() 
        {
             TotalCost = 0;
            foreach (Drink drink in Drinks)
            {
                TotalCost += drink.Cost; 
            }
            foreach (Snack snack in Snacks) 
            {
                TotalCost += snack.Cost;
            }
            return TotalCost;
        }
        
        public override string ToString()
        {
            return $"{OrderDate}\t{BuyerName}\t{Drinks}\t{Snacks}";
        }
    }
}