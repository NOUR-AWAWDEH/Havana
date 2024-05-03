using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public class TypeOfDrink
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeOfDrink(int id , string name)
        {
            Id = id;
            Name = name;

        }

        public override string ToString()
        {
            return $"{Id}{Name}";
        }

    }
}
