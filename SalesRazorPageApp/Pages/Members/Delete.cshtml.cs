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

namespace SalesRazorPageApp.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly IMemberService _memberService;

        public DeleteModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberDetailsById(id.Value);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _memberService.DeleteMember(id.Value);
            
            return RedirectToPage("./Index");
        }
    }
}
