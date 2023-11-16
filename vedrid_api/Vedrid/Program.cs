using System.Diagnostics;
using System.Net;
using Vedrid;
using Vedrid.Business;
using Vedrid.Resource;

var builder = WebApplication.CreateBuilder(args);

var vedurisUri = "https://xmlweather.vedur.is";

// Requirements for swagger
builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddSwaggerGen(x =>
{
  x.EnableAnnotations();
});

// Configure JSON logging to the console.
builder.Logging.AddJsonConsole();

builder.Services.AddHealthChecks()
	.AddUrlGroup(new Uri("https://xmlweather.vedur.is"));

// add services to dependency Injection
builder.Services.AddScoped<WeatherService>(); //Here I register my service and interface.

// add Resources to dependency Injection
builder.Services.AddScoped<IWeatherResource, VedurResource>();

// TODO Read from appsettings.json
builder.Services.AddHttpClient<IWeatherResource,VedurResource>(c => c.BaseAddress = new Uri(vedurisUri));

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

app.MapHealthChecks("/health").WithTags("System");

app.MapGet("/forecasts", async ([AsParameters] ForecastRequest forecastRequest, WeatherService weatherService, CancellationToken token) => 
{
	var language = forecastRequest.Language ?? "is";
    var results = await weatherService.GetWeatherAsync(forecastRequest.IdArray, language, forecastRequest.Time, token);

    var response = new WeatherForecastResponse{ Locations = results.Select( x => 
		new WeatherForecastLocationResponse
		{ 
			Id = x.Id, 
			Name = x.Name,
			Forecasts = x.Forecasts.Select( f => new ForecastResponse
			{
				Temperature = f.Temperature,
				Time = f.Time,
				WeatherDescription = f.WeatherDescription,
				WindDirection = f.WindDirection,
				Windspeed = f.Windspeed
			})
		}
	)};

    return Results.Ok(results);
})
.WithTags("Forecasts")
.Produces<WeatherForecastResponse>((int)HttpStatusCode.OK);

app.MapGet("/forecastlocations", async (WeatherService weatherService, CancellationToken token) => 
{
    var results = await weatherService.GetWeatherLocationsAsync(token);

	var response = new WeatherForecastResponse{ Locations = results.Select( x =>
		new WeatherForecastLocationResponse
		{ 
			Id = x.Id, 
			Name = x.Name
		})
	};

    return Results.Ok(results);
})
.WithTags("Forecasts")
.Produces<WeatherForecastResponse>((int)HttpStatusCode.OK);


app.Run();
