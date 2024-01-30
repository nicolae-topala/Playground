using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using xUnit.DAL;

namespace xUnit.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<CustomWebAppFactory>, IDisposable
    {
        protected readonly AppDbContext dbContext;
        protected readonly HttpClient httpClient;

        protected BaseIntegrationTest(CustomWebAppFactory factory)
        {
            var scope = factory.Services.CreateScope();
            httpClient = factory.CreateClient();

            dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        public void Dispose()
        {
            httpClient.Dispose();
            dbContext.Dispose();
        }
    }
}
