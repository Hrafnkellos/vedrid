using Microsoft.AspNetCore.Mvc;

namespace Vedrid;

public class ForecastRequest
{
    public ForecastRequest()
    {

    }

    [FromQuery(Name = "ids")]
    public string? Ids { get; set; }
    [FromQuery(Name = "language")]
    public string? Language { get; set; }
    [FromQuery(Name = "time")]
    public DateTime? Time { get; set; }

    public int[] IdArray => Ids?.Split(';').Select(int.Parse).ToArray() ?? Array.Empty<int>();
}
