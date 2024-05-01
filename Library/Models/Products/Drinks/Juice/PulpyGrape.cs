using Library.Models.Classes;
using Library.Models.Interfaces.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Drinks.Juice
{
    public class PulpyGrape : Drink, IJuice
    {
        public PulpyGrape(int id, string name, decimal cost, double volume) : base(id, name, cost, volume)
        {
        }
    }
}
