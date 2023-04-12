using OrdersManagement.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementAPI.Configuration
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
                options.EnableDetailedErrors();
            });

            return services;
        }
    }
}
