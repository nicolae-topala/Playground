using TaskScheduling.Shared.Interfaces;
using TaskScheduling.Shared.Services;
using TaskScheduling.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() // Microsoft.Extensions.Hosting.WindowsService for Windows
    .UseSystemd() // Microsoft.Extensions.Hosting.Systemd for Linux
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();
    })
    .Build();

host.Run();
