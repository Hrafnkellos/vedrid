namespace Vedrid;

public class ForecastResponse
{
    public DateTime Time { get; set; }
    public int Temperature { get; set; }
    public int Windspeed { get; set; }
    public string WindDirection { get; set; }
    public string WeatherDescription { get; set; }
}
