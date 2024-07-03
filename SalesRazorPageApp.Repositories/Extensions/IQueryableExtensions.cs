using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Repositories.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResultResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> query, int pageNumber,
        int pageSize) where T : class
        {
            int totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResultResponse<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalItems,
                Items = items
            };
        }
    }
}