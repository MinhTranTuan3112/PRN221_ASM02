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

namespace SalesRazorPageApp.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public EditModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var member = await _serviceFactory.MemberService.GetMemberDetailsById(id.Value);
            if (member == null)
            {
                return NotFound();
            }
            Member = member;
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

            await _serviceFactory.MemberService.UpdateMember(Member);

            return RedirectToPage("./Index");
        }
    }
}
