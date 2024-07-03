using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Services.Interfaces;

namespace SalesRazorPageApp.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public EditModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _serviceFactory.ProductService.GetProductDetailsById(id);
            Product = product;
            ViewData["CategoryId"] = new SelectList(await _serviceFactory.CategoryService.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _serviceFactory.ProductService.UpdateProduct(Product);

            return RedirectToPage("./Index");
        }

        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }

        //     _context.Attach(Product).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ProductExists(Product.ProductId))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return RedirectToPage("./Index");
        // }

    }
}
