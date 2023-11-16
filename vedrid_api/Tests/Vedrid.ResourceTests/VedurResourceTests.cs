using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Vedrid.Business;
using Vedrid.Resource;

namespace Vedrid.ResourceTests;

public class VedurResourceTests
{
    
    public  IWeatherResource WeatherResource { get; private set; }

  
    [Theory]
    [InlineData(new int[]{1,422,2642}, "is")]
    public async Task TestGetForecast(int[] ids, string language)
    {
        ILogger<VedurResource> logger = new NullLogger<VedurResource>();

        WeatherResource = new VedurResource(new HttpClient() { BaseAddress = new Uri("https://xmlweather.vedur.is") }, logger);
        
        var result = await this.WeatherResource.GetWeatherForecastsAsync(ids, language);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count());
    }

    [Theory]
    [InlineData(new int[]{1,422,2642}, "is", 3)]
    [InlineData(new int[]{1,422,2642}, "is", 1)]
    public async Task TestGetForecastWithTime(int[] ids, string language, int time)
    {
        ILogger<VedurResource> logger = new NullLogger<VedurResource>();

        WeatherResource = new VedurResource(new HttpClient() { BaseAddress = new Uri("https://xmlweather.vedur.is") }, logger);
        
        var result = await this.WeatherResource.GetWeatherForecastsAsync(ids, language,time);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task TestGetWeatherStations()
    {
        ILogger<VedurResource> logger = new NullLogger<VedurResource>();

        WeatherResource = new VedurResource(new HttpClient() { BaseAddress = new Uri("https://xmlweather.vedur.is") }, logger);
        
        var result = this.WeatherResource.GetWeatherLocations();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}