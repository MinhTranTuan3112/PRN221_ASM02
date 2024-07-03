using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesRazorPageApp.Shared.RequestModels.Product
{
    public class QueryPagedProductsRequest
    {
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }

        [FromQuery(Name = "keyword")]
        public string Keyword { get; set; } = string.Empty;
    }
}