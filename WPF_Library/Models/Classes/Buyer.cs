using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Classes
{
    public  class Buyer
    {
        int Id { get; set; }
        string Name { get; set; }
        
        public Buyer(int id , string name) 
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Id + "   " + Name ;
        }
    }
}
