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
    public class DeleteModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public DeleteModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _serviceFactory.ProductService.GetProductDetailsById(id.Value);

            if (product is null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _serviceFactory.ProductService.GetProductDetailsById(id.Value);
            if (product is not null)
            {
                Product = product;
                await _serviceFactory.ProductService.DeleteProduct(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
