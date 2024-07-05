using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Services.Interfaces;

namespace SalesRazorPageApp.Pages.Cart
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;

        private readonly IServiceFactory _serviceFactory;

        public Index(ILogger<Index> logger, IServiceFactory serviceFactory)
        {
            _logger = logger;
            _serviceFactory = serviceFactory;
        }

        public Order Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var sessionMemberId = HttpContext.Session.GetString("memberId");
            if (sessionMemberId is null)
            {
                return;
            }

            this.Order = await _serviceFactory.OrderService.GetCartInfo(Convert.ToInt32(sessionMemberId));
            
        }
    }
}