using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Entities;
namespace SalesRazorPageApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        
        Task<Product> GetProductDetailsById(int productId); 

        Task UpdateProduct(Product product);

        Task DeleteProduct(int productId);

        Task<int> CreateProduct(Product product);
    }
}