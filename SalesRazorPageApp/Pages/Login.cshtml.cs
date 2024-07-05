using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Enums;

namespace SalesRazorPageApp.Pages
{
    public class Login : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        private readonly IConfiguration _configuration;

        public Login(IServiceFactory serviceFactory, IConfiguration configuration)
        {
            _serviceFactory = serviceFactory;
            _configuration = configuration;
        }

        [BindProperty(Name = "email")]
        public string Email { get; set; } = string.Empty;

        [BindProperty(Name = "password")]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            string adminEmail = _configuration["AdminAccount:Email"]!;
            string adminPassword = _configuration["AdminAccount:Password"]!;

            if (this.Email == adminEmail && this.Password == adminPassword)
            {
                HttpContext.Session.SetString("role", Role.Admin.ToString());
                return RedirectToPage("/Index");
            }

            var member = await _serviceFactory.AuthService.Login(Email, Password);


            if (member is null)
            {
                TempData["message"] = "Wrong email or password";   
                return Page();
            }

            
            HttpContext.Session.SetString("memberId", member.MemberId.ToString());
            HttpContext.Session.SetString("role", Role.Member.ToString());

            
            return RedirectToPage("/Index");
        }
    }
}