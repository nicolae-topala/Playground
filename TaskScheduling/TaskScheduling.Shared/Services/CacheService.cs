using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TaskScheduling.Shared.Interfaces;

namespace TaskScheduling.Shared.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly string CacheWeatherKey = "Weather";


        public CacheService(IMemoryCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public void GenerateForecastCache()
        {
            var generatedList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();

            _cache.Set(CacheWeatherKey, generatedList);
            _logger.LogInformation("Cache generated.");
        }

        public List<WeatherForecast>? GetForecastCache()
        {
            var isCache = _cache.TryGetValue(CacheWeatherKey, out List<WeatherForecast>? result);
            if (!isCache)
            {
                throw new Exception("No cache generated");
            }

            return result;
        }
    }
}
