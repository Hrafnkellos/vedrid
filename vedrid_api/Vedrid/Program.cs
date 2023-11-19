using System.Diagnostics;
using System.Net;
using Vedrid;
using Vedrid.Business;
using Vedrid.Resource;
using HealthChecks.UI.Client;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var vedurisUri = new Uri(builder.Configuration.GetConnectionString("veduris"));

// Requirements for swagger
builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddSwaggerGen(x =>
{
  x.EnableAnnotations();
});

// Configure JSON logging to the console.
builder.Logging.AddJsonConsole();

builder.Services.AddHealthChecks()
	.AddUrlGroup(vedurisUri, "www.vedur.is");


builder.Services.AddHealthChecksUI(setupSettings: setup =>
    {
       setup.AddHealthCheckEndpoint("vedrid", "http://localhost:5400/healthcheck");
    }).AddInMemoryStorage();

// fix so that enums work in swagger
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// add services to dependency Injection
builder.Services.AddScoped<WeatherService>(); //Here I register my service and interface.

// add Resources to dependency Injection
builder.Services.AddScoped<IWeatherResource, VedurResource>();

builder.Services.AddHttpClient<IWeatherResource,VedurResource>(c => c.BaseAddress = vedurisUri);

var app = builder.Build();

app.Logger.LogInformation("vedrid app started");

// Add swagger ui
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.MapGet("/echo/{echo}", (string echo) => echo).WithTags("System");

app.MapGet("/healthdetails", () => {
	return Results.Ok(new {
		Name = "vedrid api",
		Process.GetCurrentProcess().StartTime,
		Host = Environment.MachineName
	});
}).WithTags("System");

app.MapHealthChecks("/healthcheck", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).WithTags("System");

app.MapHealthChecksUI(options => options.UIPath = "/dashboard")
.WithTags("System");

app.MapGet("/weather-forecasts", async ([AsParameters] ForecastRequest forecastRequest, WeatherService weatherService, CancellationToken token) => 
{
	var language = forecastRequest.Language ?? Language.IS;
	var languageString = language.ToString().ToLower();
    var results = await weatherService.GetWeatherAsync(forecastRequest.IdArray, languageString, forecastRequest.Time, token);

    return Results.Ok(results.ToForecastResponseDTO());
})
.WithTags("Weather")
.Produces<WeatherForecastResponse>((int)HttpStatusCode.OK);

app.MapGet("/weather-stations", async (WeatherService weatherService, CancellationToken token) => 
{
    var results = await weatherService.GetWeatherLocationsAsync(token);

    return Results.Ok(results.ToStationResponseDTO());
})
.WithTags("Weather")
.Produces<WeatherStationResponse>((int)HttpStatusCode.OK);

app.Run();
