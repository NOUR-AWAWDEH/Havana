using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class Snack : Product
    {
        public double Weigth { get; set; }
        
        public Snack(int id, string name, decimal cost, double weigth) : base(id, name, cost)
        {
            this.Weigth = weigth;
            
        }

        public override string ToString()
        {
            return base.ToString() + $"{Weigth}";
        }

    }
}
