﻿namespace Vedrid.Business;

public class WeatherForecastLocation
{
    /// <summary>
    /// Id of location
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name of location
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// List of forecasts
    /// </summary>
    public IEnumerable<WeatherForecast>? Forecasts { get; set; }
}
