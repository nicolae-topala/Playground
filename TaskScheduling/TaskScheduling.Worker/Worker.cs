using TaskScheduling.Shared.Interfaces;

namespace TaskScheduling.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICacheService _cacheService;

        public Worker(ILogger<Worker> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(Worker)} started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _cacheService.GenerateForecastCache();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{nameof(Worker)} error: ", ex.Message);
                }

                _logger.LogInformation("Generated cache: \n");

                var generatedCache = _cacheService.GetForecastCache();
                foreach (var cache in generatedCache)
                {
                    _logger.LogInformation(cache.ToString());
                }
                await Task.Delay(10000);
            }

            _logger.LogInformation($"{nameof(Worker)} stopped.");
        }
    }
}
