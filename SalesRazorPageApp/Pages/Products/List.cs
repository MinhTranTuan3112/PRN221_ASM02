using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.RequestModels.Product;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Pages.Products
{
    public class List : PageModel
    {
        private readonly IServiceFactory _serviceFactory;

        public List(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IList<Product> Products { get; set; } = default!;

        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 8;

        [FromQuery(Name = "totalPages")]
        public int TotalPages { get; set; } = 1;

        public PagedResultResponse<Product> PagedResult { get; set; } = default!;

        public async Task OnGetAsync(string keyword = "")
        {
            PagedResult = await _serviceFactory.ProductService.GetPagedProducts(new QueryPagedProductsRequest
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                Keyword = keyword
            });

            Products = PagedResult.Items;
        }
    }
}