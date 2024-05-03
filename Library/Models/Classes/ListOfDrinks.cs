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
        public int Id {get;set;}

        public List<Drink> Drinks { get;set;}

        public int Count { get;set;}

        public ListOfDrinks(int id, List<Drink> drinks, int Count) 
        {
            this.Id = id;
            this.Drinks = drinks;
            this.Count = Count;

        }
        
        public override string ToString() 
        {
            return $"{Id}\t{Drinks}\t{Count}";
        }
    }
}
