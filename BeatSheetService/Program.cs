using BeatSheetService.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// TODO: authorization middleware

builder.Services.AddSingleton(Log.Logger);
builder.Services.AddSingleton(typeof(IConfiguration), builder.Configuration);
//builder.Services.AddSingleton<IWeatherRepository, WeatherRepository>();
//builder.Services.AddSingleton<IOpenMeteoClient, OpenMeteoClient>();
//builder.Services.AddTransient<IWeatherService, WeatherService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO: configure CORS

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();