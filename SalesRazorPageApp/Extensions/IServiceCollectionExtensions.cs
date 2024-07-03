using Microsoft.EntityFrameworkCore;
using SalesRazorPageApp.Repositories;

namespace SalesRazorPageApp.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddWebAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextConfigurations(configuration);
            return services;
        }

        private static IServiceCollection AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SalesManagementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }


    }
}
