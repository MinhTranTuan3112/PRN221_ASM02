using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Extensions;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Shared.RequestModels.Product;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Repositories.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly SalesManagementDbContext _context;
        public ProductRepository(SalesManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResultResponse<Product>> GetPagedProducts(QueryPagedProductsRequest request)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(request.Keyword.ToLower()));
            }
            
            return await query.ToPagedResponseAsync(request.PageNumber, request.PageSize);   
        }

        public async Task<Product?> GetProductDetailsById(int productId)
        {
            return await _context.Products.Include(p => p.Category)
                                        .SingleOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _context.Products.AsNoTracking()
                                        .Include(p => p.Category)
                                        .ToListAsync();
        }
    }
}