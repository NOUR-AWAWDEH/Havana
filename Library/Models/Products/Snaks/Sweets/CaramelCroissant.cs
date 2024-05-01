using Library.Models.Classes;
using Library.Models.Interfaces.Snacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Snacks.Sweet
{
    public class CaramelCroissant : Snack, ISweet
    {
        public CaramelCroissant(int id, string name, decimal cost, double weigth) : base(id, name, cost, weigth)
        {
        }
    }
}
