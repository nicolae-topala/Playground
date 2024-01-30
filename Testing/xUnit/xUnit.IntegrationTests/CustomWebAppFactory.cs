using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using xUnit.DAL;

namespace xUnit.IntegrationTests
{
    public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithName("Integration_Test_Container")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });

                services.AddControllers();
            });
        }

        public Task InitializeAsync() => _dbContainer.StartAsync();

        public new Task DisposeAsync() => _dbContainer.DisposeAsync().AsTask();
    }
}
