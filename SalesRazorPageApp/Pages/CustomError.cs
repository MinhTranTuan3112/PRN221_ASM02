using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SalesRazorPageApp.Pages
{
    public class CustomError : PageModel
    {
        private readonly ILogger<CustomError> _logger;
        
        public string ErrorMessage { get; set; } = string.Empty;

        public int Status { get; set; }

        public CustomError(ILogger<CustomError> logger)
        {
            _logger = logger;
        }

        public void OnGet(string message, int statusCode)
        {
            ErrorMessage = message;
            Status = statusCode;
        }
    }
}