using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SalesRazorPageApp.Pages
{
    public class SignOut : PageModel
    {
        private readonly ILogger<SignOut> _logger;

        public SignOut(ILogger<SignOut> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/Login");
        }
    }
}