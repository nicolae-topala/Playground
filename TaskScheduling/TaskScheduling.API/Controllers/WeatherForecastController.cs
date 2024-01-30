using Microsoft.AspNetCore.Mvc;
using TaskScheduling.Shared.Interfaces;

namespace TaskScheduling.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
        public IActionResult Get() =>
            Ok(_cacheService.GetForecastCache());
    }
}