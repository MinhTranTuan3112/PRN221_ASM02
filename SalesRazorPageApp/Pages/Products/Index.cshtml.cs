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

namespace SalesRazorPageApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public IndexModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _serviceFactory.ProductService.GetProducts();
        }
    }
}
