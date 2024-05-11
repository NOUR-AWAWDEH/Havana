using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class TypeOfSnack
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeOfSnack(int id, string name)
        {
            Id = id;
            Name = name;

        }

        public override string ToString()
        {
            return $"{Id}\t{Name}";
        }
    }
}
