using Microsoft.EntityFrameworkCore;
using RestApp.BL.Interfaces;
using RestApp.BL.Services;
using RestApp.Dal.Interfaces;
using RestApp.Dal.Repositories;
using RestApp.DemoMigrations;
using RestApp.SecondaryDbMigrations;

namespace RestApp.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DemoDatabaseContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("RestAppConnection")));

            services.AddDbContext<SecondaryDatabaseContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("RestAppSecondaryConnection")));
        }

        public static void AddDependencyInjections(this IServiceCollection services, IConfiguration configuration) {
            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                options.Configuration = connectionString;
            });

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
