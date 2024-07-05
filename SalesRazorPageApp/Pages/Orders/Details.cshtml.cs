using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Services.Interfaces;

namespace SalesRazorPageApp.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public DetailsModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var order = await _serviceFactory.OrderService.GetOrderDetailsInfoById(id.Value);
            if (order is null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            
            return Page();
        }
    }
}
