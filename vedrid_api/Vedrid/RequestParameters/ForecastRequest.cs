using Microsoft.AspNetCore.Mvc;

namespace Vedrid;

public class ForecastRequest
{
    public ForecastRequest()
    {

    }

    /// <summary>
    /// Id of locations we want to forecast for
    /// </summary>
    [FromQuery(Name = "ids")]
    public string? Ids { get; set; }
    /// <summary>
    /// In what language should the Response be
    /// </summary>
    [FromQuery(Name = "language")]
    public string? Language { get; set; }
    /// <summary>
    /// How much time between forecasts
    /// </summary>
    [FromQuery(Name = "timeInterval")]
    public int? Time { get; set; }

    public int[] IdArray => Ids?.Split(';').Select(int.Parse).ToArray() ?? Array.Empty<int>();
}
