﻿namespace Vedrid;

public class Forecast
{
    public DateTime Time { get; set; }
    public int Temperature { get; set; }
    public int Windspeed { get; set; }
    public string WindDirection { get; set; }
    public string WeatherDescription { get; set; }
}
