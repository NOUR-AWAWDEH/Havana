using Library.Models.Classes;
using Library.Models.Interfaces.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Drinks.Water
{
    public class WaterWithGas : Drink, IWater
    {
        public WaterWithGas(int id, string name, float cost, float volume) : base(id, name, cost, volume)
        {
        }
    }
}
