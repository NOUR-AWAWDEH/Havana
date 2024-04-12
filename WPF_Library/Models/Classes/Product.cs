using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public abstract class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Cost { get; set; }

        public Product(int id, string name,float cost) 
        {
            this.Id = id;
            this.Name = name;
            this.Cost = cost;
        }
        //public abstract void SearchByName();
        //public abstract void SearchByCost();
        public virtual new string ToString()
        {
            return $"{Id}\t{Name}\t{Cost}";
        }
    }
}
