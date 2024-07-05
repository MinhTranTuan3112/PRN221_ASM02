using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories;

namespace SalesRazorPageApp.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWebAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextConfigurations(configuration)
                    .AddSessionConfigurations();
            return services;
        }

        private static IServiceCollection AddSessionConfigurations(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1); // Session timeout.
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            return services;
        }

        private static IServiceCollection AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SalesManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }


    }
}
