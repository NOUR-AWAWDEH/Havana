﻿using Library.Models.Classes;
using Library.Models.Interfaces.Snacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Library.Models.Products.Snacks.Cold
{
    public class CajunChickenWithLimeRice : Snack, IHot
    {
        public CajunChickenWithLimeRice(int id, string name, float cost, float Weigth) : base(id, name, cost, Weigth)
        {
        }
    }
}