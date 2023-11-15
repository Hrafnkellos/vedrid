using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Vedrid.Business;
using Vedrid.Resource;

namespace Vedrid.ResourceTests;

public class WeatherServiceFixture : IDisposable
{
    public  IWeatherResource WeatherResource { get; private set; }

    public WeatherServiceFixture()
    {
        ILogger<VedurResource> logger = new NullLogger<VedurResource>();

        WeatherResource = new VedurResource(new HttpClient() { BaseAddress = new Uri("https://xmlweather.vedur.is") }, logger);
    }

    public void Dispose()
    {

    }
}


[CollectionDefinition("WeatherService collection")]
public class WeatherServiceCollection : ICollectionFixture<WeatherServiceFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}