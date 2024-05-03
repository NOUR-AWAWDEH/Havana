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

        public decimal Cost { get; set; }

        public Product(int id, string name, decimal cost) 
        {
            this.Id = id;
            this.Name = name;
            this.Cost = cost;
        }
       
        public virtual new string ToString()
        {
            return $"{Id}{Name}{Cost}";
        }
    }
}
