using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Shared.RequestModels.Product;
using SalesRazorPageApp.Shared.ResponseModels.Query;

namespace SalesRazorPageApp.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetProductDetailsById(int productId);

        Task<List<Product>> GetProductsWithCategory();

        Task<PagedResultResponse<Product>> GetPagedProducts(QueryPagedProductsRequest request);
    }
}