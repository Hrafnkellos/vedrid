namespace Vedrid;

public record WeatherStationResponse
{
    public IEnumerable<WeatherStation> WeatherStations { get; init; } = Enumerable.Empty<WeatherStation>();
}