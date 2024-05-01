using Library.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Models.Classes 
{
    public class ReportGenerator
    {
        public List<Order> OrderList { get; set; }

        public ReportGenerator(List<Order> orderList)
        {
            this.OrderList = orderList;

        }

        public void GenerateSummaryReport()
        {

        }

        
    }
}
