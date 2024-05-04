using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class Drink : Product
    {
        public double Volume { get; set; }

        public int TypeOfDrinkId { get;set;}

        public int Count { get; set; } 


        public Drink(int id, string name, decimal cost, double volume ) : base(id, name, cost)
        {
            Volume = volume;
        }

        public Drink(int id, string name, decimal cost, double volume, int typeOfDrinkId) : base(id, name, cost)
        {
            Volume = volume;
            TypeOfDrinkId = typeOfDrinkId;
        }

        
        public Drink(int id, string name, int countofDrinks, decimal cost, double volume) : base(id, name, cost)
        {
            Volume = volume;
            this.Count = countofDrinks;  
        }

        public override string ToString()
        {
            return base.ToString() + $"{Volume}";
        }

    }
}
