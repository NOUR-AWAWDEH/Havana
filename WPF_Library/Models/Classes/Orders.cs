using Library.Models.Interfaces.Drinks;
using System;
using System.Collections.Generic;

namespace Library.Models.Classes
{
    public class Orders
    {
        public DateTime OrderDate { get; set; }

        public Buyer BuyerName { get; set; }

        public Dictionary<int, Product> Products { get; set; } // Key Represent 

        public List<Drink> Drinks { get; set; }

        public List<Snack> Snacks { get; set; }

        public Orders(DateTime orderDate, Buyer buyerName, Dictionary<int, Product> products, List<Drink> drinks, List<Snack> snacks)
        {
            OrderDate = orderDate;
            BuyerName = buyerName;
            Products = products;
            Drinks = drinks;
            Snacks = snacks;
        }

        public Product GetProduct(int key)
        {
            if (Products.ContainsKey(key))
            {
                return Products[key];
            }

            return null;
        }

        public void SetProduct(int key, Product product)
        {
            Products[key] = product;
        }

        public override string ToString()
        {
            return $"{OrderDate}\t{BuyerName}\t{Products}\t{Drinks}\t{Snacks}";
        }
    }
}