using BeatSheetService.Extensions;
using BeatSheetService.Middleware;
using BeatSheetService.Repositories;
using BeatSheetService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// pull env vars into IConfiguration
builder.Configuration.AddEnvironmentVariables();

// config logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// TODO: authorization middleware

// register logger and configuration for DI
builder.Services.AddSingleton(Log.Logger);
builder.Services.AddSingleton(typeof(IConfiguration), builder.Configuration);

// configure and register mongo DB beat sheet collection for DI
builder.Services.ConfigureDatabase(builder.Configuration);

// register services/repositories for DI
builder.Services.AddSingleton<IBeatSheetService, BeatSheetService.Services.BeatSheetService>();
builder.Services.AddTransient<IBeatService, BeatService>();
builder.Services.AddTransient<IActService, ActService>();
builder.Services.AddSingleton<IBeatSheetRepository, BeatSheetRepository>();

// register controllers and swagger
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