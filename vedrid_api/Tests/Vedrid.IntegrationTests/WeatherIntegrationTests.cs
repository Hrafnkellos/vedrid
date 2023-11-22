using FluentAssertions;

namespace Vedrid.IntegrationTests;

public class WeatherIntegrationTests 
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var api = new ApiWebApplicationFactory();
        var response = await api.CreateClient().GetAndDeserialize<WeatherForecastResponse>("/weather-forecasts");
        response.Stations.Should().HaveCount(5);
    }

        [Fact]
    public async Task GET_retrieves_weather_stations()
    {
        var api = new ApiWebApplicationFactory();
        var response = await api.CreateClient().GetAndDeserialize<WeatherStationResponse>("/weather-stations");
        response.WeatherStations.Should().HaveCount(5);
        response.WeatherStations.Should().NotContain(x => x.Id == null);
    }
}