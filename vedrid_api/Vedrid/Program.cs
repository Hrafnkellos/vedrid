using Microsoft.AspNetCore.Mvc;
using Vedrid;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Ok());

app.MapGet("/echo", (string echo) => echo);

app.MapGet("/forecasts", async ([FromQuery] ForecastRequest forecastRequest, CancellationToken token) => 
{
    return Results.Ok();
});


app.Run();
