﻿using Library.Models.Classes;
using Library.Models.Interfaces.Snacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Products.Snacks.Cold
{
    public class DoritosNachoWithCheese : Snack, IHot
    {
        public DoritosNachoWithCheese(int id, string name, float cost, float weigth) : base(id, name, cost, weigth)
        {
        }
    }
}
