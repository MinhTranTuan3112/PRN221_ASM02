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
using SalesRazorPageApp.Shared.Enums;

namespace SalesRazorPageApp.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public EditModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _serviceFactory.OrderService.GetOrderDetailsInfoById(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            Order = order;
            
            ViewData["MemberId"] = new SelectList(await _serviceFactory.MemberService.GetMembers(), "MemberId", "Email");

            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>(), Order.Status);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _serviceFactory.OrderService.UpdateOrder(Order);

            return RedirectToPage("./Index");
        }

    }
}
