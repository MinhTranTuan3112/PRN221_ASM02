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
using SalesRazorPageApp.Shared.RequestModels.Order;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public IndexModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 10;

        public IList<Order> Order { get; set; } = default!;

        public PagedResultResponse<Order> PagedResult { get; set; } = default!;

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            PagedResult = await _serviceFactory.OrderService.GetPagedOrders(new QueryPagedOrderRequest
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                StartDate = startDate,
                EndDate = endDate
            });

            Order = PagedResult.Items;
        }
    }
}
