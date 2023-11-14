namespace Vedrid.Business;

public interface IWeatherResource
{
    public IEnumerable<WeatherForecast> GetWeatherForecastsAsync(IEnumerable<int> ids, string? language, CancellationToken? cancellationToken = default);
}
 