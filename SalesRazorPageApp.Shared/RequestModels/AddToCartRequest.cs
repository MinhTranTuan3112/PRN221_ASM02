using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Shared.RequestModels
{
    public class AddToCartRequest
    {
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}