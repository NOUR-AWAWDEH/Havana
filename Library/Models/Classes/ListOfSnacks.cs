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

        public ListOfSnacks(int id , List<Snack> snacks, int count) 
        {
            this.Id = id;
            this.Snacks = snacks;
            this.Count = count;

        }

        public override string ToString() 
        {
            return $"{Id}\t{Snacks}\t{Count}";
        }

    }
}
