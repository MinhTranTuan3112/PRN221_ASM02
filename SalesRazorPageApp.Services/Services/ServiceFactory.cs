using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Services.Interfaces;

namespace SalesRazorPageApp.Services.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IMemberService> _memberService;

        public ServiceFactory(IUnitOfWork unitOfWork)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(unitOfWork));
            _authService = new Lazy<IAuthService>(() => new AuthService(unitOfWork));
            _memberService = new Lazy<IMemberService>(() => new MemberService(unitOfWork));
        }

        public IProductService ProductService => _productService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IAuthService AuthService => _authService.Value;

        public IMemberService MemberService => _memberService.Value;
    }
}