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

        public int TypeOfSnakId { get; set; }

        public Snack(int id, string name, decimal cost, double weigth) : base(id, name, cost)
        {
            this.Weigth = weigth;
            
        }

        public Snack(int id, string name, decimal cost, double weigth,int typeOfSnackId ) : base(id, name, cost)
        {
            this.Weigth = weigth;
            this.TypeOfSnakId = typeOfSnackId;

        }

        
        public override string ToString()
        {
            return base.ToString() + $"\t{Weigth}" ;
        }

    }
}
