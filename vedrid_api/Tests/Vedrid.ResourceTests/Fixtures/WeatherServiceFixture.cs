using Vedrid.Business;

namespace Vedrid.ResourceTests;

public class WeatherServiceFixture : IDisposable
{
    public  IWeatherResource WeatherResource { get; private set; }

    public WeatherServiceFixture()
    {
        WeatherResource = new WeatherResource();
    }

    public void Dispose()
    {

    }
}
