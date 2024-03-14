using AzureFunctionsApp.Functions.Middlewares;
using AzureFunctionsApp.DAL;
using AzureFunctionsApp.DAL.Interfaces;
using AzureFunctionsApp.DAL.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string? sqlConnectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(worker =>
    {
        worker.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(sqlConnectionString);
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();
    })
    .Build();

host.Run();
