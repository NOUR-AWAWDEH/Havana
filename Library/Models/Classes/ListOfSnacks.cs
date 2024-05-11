using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class ListOfSnacks
    {
        public int Id { get; set; }
        public List<Snack> Snacks { get; set; }
        public int Count { get; set; }

        public ListOfSnacks(int id ,int count) 
        {
            this.Id = id;
            this.Snacks = new List<Snack>();
            this.Count = count;

        }

        public ListOfSnacks() { }

        public override string ToString() 
        {
            return $"{Id}\t{Snacks}\t{Count}";
        }

    }
}
