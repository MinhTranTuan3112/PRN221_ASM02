using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Shared.ResponseModels.Query
{
    public class PagedResultResponse<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
        public List<T> Items { get; set; } = [];   
        
    }
}