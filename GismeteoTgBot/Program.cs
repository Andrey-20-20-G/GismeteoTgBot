using GismeteoTgBot;
using GismeteoTgBot.WeatherService.Parser;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<WeatherParser>();

var host = builder.Build();
host.Run();
