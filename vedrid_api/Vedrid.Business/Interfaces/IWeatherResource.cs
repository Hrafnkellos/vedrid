namespace Vedrid.Business;

public interface IWeatherResource
{
    public Task<IEnumerable<WeatherForecastLocation>> GetWeatherLocationsAsync(CancellationToken? cancellationToken = default);

    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(IEnumerable<int> ids, string? language, CancellationToken? cancellationToken = default);
}
 