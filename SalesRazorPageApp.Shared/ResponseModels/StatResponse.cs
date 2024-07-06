using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Shared.ResponseModels
{
    public class StatResponse
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TotalSales { get; set; }
    }
}