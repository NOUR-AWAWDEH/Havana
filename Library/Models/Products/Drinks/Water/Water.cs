using Library.Models.Classes;
using Library.Models.Interfaces.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Drinks.Water
{
    public class Water : Drink, IWater
    {
        public Water(int id, string name, decimal cost, double volume) : base(id, name, cost, volume)
        {
        }
    }
}
