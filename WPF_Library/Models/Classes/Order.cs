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

        public Order(DateTime orderDate, Buyer buyerName, List<Drink> drinks, List<Snack> snacks)
        {
            OrderDate = orderDate;
            BuyerName = buyerName;
            Drinks = drinks;
            Snacks = snacks;
        }

        

        public override string ToString()
        {
            return $"{OrderDate}\t{BuyerName}\t{Drinks}\t{Snacks}";
        }
    }
}