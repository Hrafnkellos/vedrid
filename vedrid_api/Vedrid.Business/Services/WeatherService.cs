﻿namespace Vedrid.Business;
using Microsoft.Extensions.Logging;

public class WeatherService
{
    private readonly IWeatherResource weatherResource;
    private readonly ILogger<WeatherService> logger;

    public WeatherService(IWeatherResource weatherResource, ILogger<WeatherService> logger) 
    {
        this.weatherResource = weatherResource;
    }

    public async Task<IEnumerable<WeatherForecastLocation>> GetWeatherAsync(IEnumerable<int> ids, string language, int? time, CancellationToken? cancellationToken) 
    {
        return await this.weatherResource.GetWeatherForecastsAsync(ids, language, time, cancellationToken);
    }

    public async Task<IEnumerable<WeatherForecastLocation>> GetWeatherLocationsAsync(CancellationToken? cancellationToken) 
    {
        return this.weatherResource.GetWeatherLocations();
    }
}
