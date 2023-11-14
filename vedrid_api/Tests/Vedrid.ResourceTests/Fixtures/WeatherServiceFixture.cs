using Vedrid.Business;
using Vedrid.Resource;

namespace Vedrid.ResourceTests;

public class WeatherServiceFixture : IDisposable
{
    public  IWeatherResource WeatherResource { get; private set; }

    public WeatherServiceFixture()
    {
        WeatherResource = new VedurResource();
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