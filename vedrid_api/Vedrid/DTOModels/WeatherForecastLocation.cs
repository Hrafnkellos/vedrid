namespace Vedrid;

public record WeatherForecastLocation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime FromTime { get; set; }
    public IEnumerable<Forecast> Forecasts { get; set; }
}
