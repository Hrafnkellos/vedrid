using Vedrid.Business;

namespace Vedrid.Resource;

public class VedurResource : IWeatherResource
{
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync(IEnumerable<int> ids, string? language, CancellationToken? cancellationToken = null)
    {
        using var client = new HttpClient();
        string basePath = "https://xmlweather.vedur.is?op_w=xml&type=forec";
        string rest = "&lang=is&view=xml&ids=1;422";

        var result = await client.GetAsync(basePath +rest);
        Console.WriteLine(result.StatusCode);

        return null;
    }

    public Task<IEnumerable<WeatherForecastLocation>> GetWeatherLocationsAsync(CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
}
