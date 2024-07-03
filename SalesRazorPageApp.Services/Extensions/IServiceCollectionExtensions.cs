using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SalesRazorPageApp.Repositories.Interfaces;
using SalesRazorPageApp.Repositories.Repositories;
using SalesRazorPageApp.Services.Interfaces;
using SalesRazorPageApp.Services.Services;

namespace SalesRazorPageApp.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMemberService, MemberService>();
            return services;
        }
    }
}
