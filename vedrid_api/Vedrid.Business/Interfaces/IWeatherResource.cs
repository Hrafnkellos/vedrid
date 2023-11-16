namespace Vedrid.Business;

public interface IWeatherResource
{
    public IEnumerable<WeatherForecastLocation> GetWeatherLocations();

    public Task<IEnumerable<WeatherForecastLocation>> GetWeatherForecastsAsync(IEnumerable<int> ids, string language, int? time = null, CancellationToken? cancellationToken = default);
}
 