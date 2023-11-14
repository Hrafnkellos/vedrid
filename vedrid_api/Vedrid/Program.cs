using Vedrid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();     
builder.Services.AddSwaggerGen(x =>
{
  x.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.MapGet("/echo", (string echo) => echo).WithTags("System");

app.MapGet("/healthcheck", (string echo) => echo).WithTags("System");

app.MapGet("/forecasts", async ([AsParameters] ForecastRequest forecastRequest, CancellationToken token) => 
{
    return Results.Ok();
})
.WithTags("Forecasts");


app.Run();
