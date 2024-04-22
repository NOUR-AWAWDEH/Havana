using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Library.Models.Classes
{
    public class DrinkPhoto
    {
        public int Id { get; set; }
        public ImageSource Image { get; set; }
        public Drink Drink { get; set; }

        public DrinkPhoto(int id,ImageSource image ,Drink drink) 
        {
            Id = id;
            Image = image;  
            Drink = drink;
        }

        public override string ToString() 
        {
            return $"{Id}\t{Drink}";
        }
    }
}
