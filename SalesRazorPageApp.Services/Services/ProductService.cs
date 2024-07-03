using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using SalesRazorPageApp.Repositories.Entities;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Shared.Exceptions;

namespace SalesRazorPageApp.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateProduct(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product.ProductId;
        }

        public async Task DeleteProduct(int productId)
        {
            if (!await _unitOfWork.ProductRepository.AnyAsync(p => p.ProductId == productId))
            {
                throw new NotFoundException("Product not found");
            }

            await _unitOfWork.ProductRepository.ExecuteDeleteAsync(p => p.ProductId == productId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Product> GetProductDetailsById(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductDetailsById(productId);

            return (product is not null) ? product : throw new NotFoundException("Product not found");
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _unitOfWork.ProductRepository.GetProductsWithCategory();
        }

        public async Task UpdateProduct(Product product)
        {
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}