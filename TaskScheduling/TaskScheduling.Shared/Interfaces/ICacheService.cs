namespace TaskScheduling.Shared.Interfaces
{
    public interface ICacheService
    {
        void GenerateForecastCache();
        List<WeatherForecast>? GetForecastCache();
    }
}
