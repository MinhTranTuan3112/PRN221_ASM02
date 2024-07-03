using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Repositories.Repositories;

namespace SalesRazorPageApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesManagementDbContext _context;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        
        private readonly Lazy<IMemberRepository> _memberRepository;

        public UnitOfWork(SalesManagementDbContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
            _memberRepository = new Lazy<IMemberRepository>(() => new MemberRepository(context));
        }

        public IProductRepository ProductRepository => _productRepository.Value;

        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IMemberRepository MemberRepository => _memberRepository.Value;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}