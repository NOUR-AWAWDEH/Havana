
using System;
using System.Collections.Generic;
using System.Deployment.Internal;

namespace Library.Models.Classes
{
    public class Order
    {
        public int Id { get; set; } 
        public DateTime DateTime { get; set; }

        public Buyer BuyerName { get; set; }

        public ListOfDrinks DrinksList { get; set; }

        public ListOfSnacks SnacksList { get; set; }

        public decimal TotalCost {get;set;}

        public Order() { }

        public Order(int id,DateTime dateTime, Buyer buyerName, ListOfDrinks drinksList, ListOfSnacks snacksList, decimal totalCost)
        {
            Id = id;
            DateTime = dateTime;
            BuyerName = buyerName;
            DrinksList = drinksList;
            SnacksList = snacksList;
            TotalCost = totalCost;
        }



        public int CalculateCoutOfSnacks() 
        {
            SnacksList.Count = 0;

            foreach (Snack snack in SnacksList.Snacks)
            {
                SnacksList.Count++;
            }
            return SnacksList.Count;
        }

        public int CalculateCoutOfDrinks()
        {
            DrinksList.Count = 0;

            foreach (Drink drink in DrinksList.Drinks)
            {
                DrinksList.Count++;
            }
            return DrinksList.Count;
        }

        public decimal CalculateTotalCost() 
        {
             TotalCost = 0;
            foreach (Drink drink in DrinksList.Drinks)
            {
                TotalCost += drink.Cost;
                 
            }
            foreach (Snack snack in SnacksList.Snacks) 
            {
                TotalCost += snack.Cost;
                
            }
            return TotalCost;
        }
        
        public override string ToString()
        {
            return $"{Id}\t{DateTime}\t{BuyerName}\t{DrinksList}\t{SnacksList}\t{TotalCost}";
        }
    }
}