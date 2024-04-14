using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class Drink : Product
    {
        public float Volume { get; set; }
       
        public Drink(int id, string name, float cost, float volume ) : base(id, name, cost)
        {
            Volume = volume;
            
        }

        public override string ToString()
        {
            return base.ToString()+ $"{Volume}";
        }

    }
}
