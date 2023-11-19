namespace Vedrid;

public record WeatherStation : WeatherStationBase
{
    public DateTime FromTime { get; set; }
    public IEnumerable<Forecast>? Forecasts { get; set; }
}
