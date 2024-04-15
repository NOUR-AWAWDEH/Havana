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
       
        public Drink(int id, string name, decimal cost, double volume ) : base(id, name, cost)
        {
            Volume = volume;
            
        }

        public override string ToString()
        {
            return base.ToString()+ $"{Volume}";
        }

    }
}
