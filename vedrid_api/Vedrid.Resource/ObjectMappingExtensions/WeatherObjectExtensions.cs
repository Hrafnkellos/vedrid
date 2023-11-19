using Vedrid.Business;

namespace Vedrid.Resource;

public static class WeatherObjectExtensions
{
    public static IEnumerable<WeatherForecastLocation> ToWeatherModel(this forecasts forecasts)
    {
        return forecasts.Items.Select(s => s.ToWeatherLocationModel());
    }

    public static WeatherForecastLocation ToWeatherLocationModel(this forecastsStation forecastsStation)
    {
        return new WeatherForecastLocation
        {
            Id = Convert.ToInt32(forecastsStation.id),
            Name = forecastsStation.name,
            FromTime = DateTime.Parse(forecastsStation.atime),
            Forecasts = forecastsStation.forecast.Select(f => f.ToWeatherForecastModel())
        };
    }

    public static WeatherForecast ToWeatherForecastModel(this forecastsStationForecast forecast)
    {
        return new WeatherForecast
        {
            Time = DateTime.Parse(forecast.ftime),
            Temperature = Convert.ToInt32(forecast.T),
            WeatherDescription = forecast.W,
            WindDirection = forecast.D,
            WindSpeed = Convert.ToInt32(forecast.F)
        };
    }
}
