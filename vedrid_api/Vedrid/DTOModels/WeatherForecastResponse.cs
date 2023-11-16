namespace Vedrid;

public record WeatherForecastResponse
{
    public IEnumerable<WeatherForecastLocation> Locations { get; set; }
}
