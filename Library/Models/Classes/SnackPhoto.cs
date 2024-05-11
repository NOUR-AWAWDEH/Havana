using Library.Models.Classes;
using System;
using System.Windows.Media;

namespace Library.Models.Classes
{
    public class SnackPhoto
    {      
        public int Id { get; set; }

        public  ImageSource  Image{ get; set; }

        public Snack Snack { get; set; }

        public SnackPhoto(int id, ImageSource image, Snack snack)
        {
            Id = id;
            Image  = image;
            Snack = snack;
        }
        
        public override string ToString()
        {
            return $"{Snack.Id}\t{Snack.Name}\t{Snack.Cost}\t{Snack.Weigth} ";
        }
    }
}