using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class DrinkPhoto
    {
        public int Id { get; set; }
        public Drink Drink { get; set; }

        public DrinkPhoto(int id, Drink drink) 
        {
            Id = id;
            Drink = drink;
        }

        public override string ToString() 
        {
            return $"{Id}\t{Drink}";
        }
    }
}
