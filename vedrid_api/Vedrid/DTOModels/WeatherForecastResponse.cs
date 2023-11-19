namespace Vedrid;

public record WeatherForecastResponse
{
    public IEnumerable<WeatherStation>? Stations { get; set; }
}
