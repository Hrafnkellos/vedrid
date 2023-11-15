namespace Vedrid.ResourceTests;

public class VedurResourceTests
{
    private WeatherServiceFixture weatherServiceFixture;

    public VedurResourceTests(WeatherServiceFixture weatherServiceFixture)
    {
        this.weatherServiceFixture = weatherServiceFixture;
    }
  
    [Theory]
    [InlineData(new int[]{1,422,2642}, "is", "")]
    public async Task TestGetForecast(int[] ids, string language, string time)
    {
        var result = await this.weatherServiceFixture.WeatherResource.GetWeatherForecastsAsync(ids, language);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count());
    }
}