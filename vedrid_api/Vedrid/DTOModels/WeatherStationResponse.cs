namespace Vedrid;

public record WeatherStationResponse
{
    public IEnumerable<WeatherStationBase>? WeatherStations { get; init; } = Enumerable.Empty<WeatherStation>();
}