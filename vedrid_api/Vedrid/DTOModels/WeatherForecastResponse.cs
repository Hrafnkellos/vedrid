using Vedrid.Business;

namespace Vedrid;

public record WeatherForecastResponse
{
    public IEnumerable<WeatherForecastLocationResponse> Locations { get; set; }
}
