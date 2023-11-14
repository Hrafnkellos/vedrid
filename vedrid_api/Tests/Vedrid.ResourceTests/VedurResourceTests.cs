namespace Vedrid.ResourceTests;

public class VedurResourceTests
{
    WeatherServiceFixture weatherServiceFixture;

    public VedurResourceTests(WeatherServiceFixture weatherServiceFixture)
    {
        this.weatherServiceFixture = weatherServiceFixture;
    }

    [Theory]
    [InlineData(0, "is", "")]
    public async Task TestGetForecast(int id, string language, string time)
    {
        await this.weatherServiceFixture.WeatherResource.GetWeatherForecastsAsync();
    }
}