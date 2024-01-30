using TaskScheduling.Shared.Interfaces;

namespace TaskScheduling.API.Services
{
    public class WeatherCacheRefresherBackgroundService : BackgroundService
    {
        private readonly ICacheService _cacheService;

        private readonly ILogger<WeatherCacheRefresherBackgroundService> _logger;

        public WeatherCacheRefresherBackgroundService(ICacheService cacheService, ILogger<WeatherCacheRefresherBackgroundService> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _cacheService.GenerateForecastCache();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{nameof(WeatherCacheRefresherBackgroundService)} error: ", ex.Message);
                }

                await Task.Delay(3000);
            }

            _logger.LogInformation($"{nameof(WeatherCacheRefresherBackgroundService)} stopped.");
        }
    }
}
