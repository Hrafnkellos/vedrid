using Vedrid.Business;

namespace Vedrid;

public static class DTOExtensions
{
    public static WeatherForecastResponse ToForecastResponseDTO(this IEnumerable<WeatherForecastLocation> forecasts)
    {
        return new WeatherForecastResponse{ Stations = forecasts.Select(s => s.ToWeatherLocationModel())};
    }

    public static WeatherStation ToWeatherLocationModel(this WeatherForecastLocation forecastsStation)
    {
        return new WeatherStation
        {
            Id = forecastsStation.Id,
            Name = forecastsStation.Name,
            FromTime = forecastsStation.FromTime,
            Forecasts = forecastsStation.Forecasts?.Select(f => f.ToWeatherForecastDTO())
        };
    }

    public static Forecast ToWeatherForecastDTO(this WeatherForecast forecast)
    {
        return new Forecast
        {
            Temperature = forecast.Temperature,
            Time = forecast.Time,
            WeatherDescription = forecast.WeatherDescription,
            WindDirection = forecast.WindDirection,
            WindSpeed = forecast.WindSpeed
        };
    }

    public static WeatherStationResponse ToStationResponseDTO(this IEnumerable<WeatherForecastLocation> locations)
    {
    	return new WeatherStationResponse{ WeatherStations = locations.Select( x =>
            new WeatherStationBase
            { 
                Id = x.Id, 
                Name = x.Name
            })
        };
    }
}
