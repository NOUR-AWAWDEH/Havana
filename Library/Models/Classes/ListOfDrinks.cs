using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class ListOfDrinks
    {
        public int Id { get; set; }

        public List<Drink> Drinks { get; set; }
        public int Count { get;set;}

        
        public ListOfDrinks(int id, int Count) 
        {
            this.Id = id;
            this.Drinks = new List<Drink>();
            this.Count = Count;

        }

        public ListOfDrinks() { }

        public override string ToString() 
        {
            return $"{Id}{Drinks}{Count}";
        }
    }
}
