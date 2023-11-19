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

    public async Task<IEnumerable<WeatherForecastLocation>> GetWeatherForecastsAsync(IEnumerable<int> ids, string language, int? time = null, CancellationToken? cancellationToken = null)
    {
        // We have to think about errors here but there is not time to do that now
        if (ids == null || !ids.Any())
        {
            ids = this.GetWeatherLocations().Select(x => x.Id);
        }
        
        // we could document this query better and use some query builder
        string rest = $"?op_w=xml&type=forec&lang={language}&view=xml&ids={string.Join(";", ids)}";

        if (time != null)
        {
            rest += $"&time={time}h";
        }

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

    public IEnumerable<WeatherForecastLocation> GetWeatherLocations()
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
