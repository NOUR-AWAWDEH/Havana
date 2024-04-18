using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Library.Models.Classes
{
    public class SnackPhoto
    {
        public int Id { get; set; } 
        public Snack Snack { get; set; }

        public SnackPhoto(int id , Snack snack) 
        {
            Id = id;
            Snack = snack;
        }

        public override string ToString() 
        {
            return $"{Id}\t{Snack}";
        }
    }
}
