﻿namespace Vedrid.Business;

public interface IWeatherResource
{
    public Task<IEnumerable<WeatherForecastLocation>> GetWeatherLocationsAsync(CancellationToken? cancellationToken = default);

    public Task<IEnumerable<WeatherForecastLocation>> GetWeatherForecastsAsync(IEnumerable<int> ids, string? language, CancellationToken? cancellationToken = default);
}
 