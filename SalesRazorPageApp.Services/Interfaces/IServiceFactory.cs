using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Services.Interfaces
{
    public interface IServiceFactory
    {
        IProductService ProductService { get; }

        ICategoryService CategoryService { get; }

        IAuthService AuthService { get; }

        IMemberService MemberService { get; }
    }
}