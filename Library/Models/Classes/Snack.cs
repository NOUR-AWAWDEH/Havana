using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Library.Models.Classes
{
    public class Snack : Product
    {
        public double Weight { get; set; }

        public int TypeOfSnakId { get; set; }

        public int Count { get; set; }

        public Snack(int id, string name, decimal cost, double weight) : base(id, name, cost)
        {
            this.Weight = weight;
            
        }

        public Snack(int id, string name, decimal cost, double weight, int typeOfSnackId ) : base(id, name, cost)
        {
            this.Weight = weight;
            this.TypeOfSnakId = typeOfSnackId;

        }
        public Snack(int id, string name,int count, decimal cost, double weight) : base(id, name, cost)
        {
            this.Weight = weight;
            this.Count = count;

        }

        public override string ToString()
        {
            return base.ToString() + $"\t{Weight}" ;
        }

    }
}
