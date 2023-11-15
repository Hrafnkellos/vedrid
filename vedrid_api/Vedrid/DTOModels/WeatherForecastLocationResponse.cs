using System.Text.Json.Serialization;

namespace Vedrid;

public record WeatherForecastLocationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ForecastResponse> Forecasts { get; set; }
}
