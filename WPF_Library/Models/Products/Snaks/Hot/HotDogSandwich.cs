﻿using Library.Models.Classes;
using Library.Models.Interfaces.Snacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Snacks.Hot
{
    public class HotDogSandwich : Snack, IHot
    {
        public HotDogSandwich(int id, string name, decimal cost, double weigth) : base(id, name, cost, weigth)
        {
        }
    }
}
