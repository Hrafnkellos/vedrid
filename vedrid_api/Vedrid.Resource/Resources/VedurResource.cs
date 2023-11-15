using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Vedrid.Business;


namespace Vedrid.Resource;

public class VedurResource : IWeatherResource
{
    private readonly HttpClient vedurHttpClient;
    private readonly ILogger<VedurResource> logger;

    public VedurResource(HttpClient client, ILogger<VedurResource> logger)
    {
        this.vedurHttpClient = client;
        this.logger = logger;
    }

    public async Task<IEnumerable<WeatherForecastLocation>> GetWeatherForecastsAsync(IEnumerable<int> ids, string? language, CancellationToken? cancellationToken = null)
    {
        // We have to think about errors here but there is not time to do that now
        string rest = $"?op_w=xml&type=forec&lang=is&view=xml&ids=1;422";
        rest = $"?op_w=xml&type=forec&lang={language}&view=xml&ids={string.Join(";", ids)}";

        try
        {
            var response = await this.vedurHttpClient.GetAsync(rest);

            var returnedXml = await response.Content.ReadAsStringAsync();
            var forecasts = returnedXml.ParseXML<forecasts>();

            return forecasts.ToWeatherModel();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error came up when fetching data from vedur.is");
            throw;
        }
    }

    public async Task<IEnumerable<WeatherForecastLocation>> GetWeatherLocationsAsync(CancellationToken? cancellationToken = null)
    {
        // TODO add a chron scrapper that inserts ids into DB/cache. then fetch that data here.
        // So we just return mock data
        return new List<WeatherForecastLocation>
        {
            new() { Id = 1, Name = "Reykjavík" },
            new() { Id = 422, Name = "Akureyri" },
            new() { Id = 2642, Name = "Ísafjörður" },
            new() { Id = 571, Name = "Egilsstaðir" },
            new() { Id = 5544, Name = "Höfn í Hornafirði" },
        };
    }
}
