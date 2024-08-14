using GismeteoTgBot;
using GismeteoTgBot.BotSettings.Models;
using GismeteoTgBot.WeatherService.Parser;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TgBaseModel>();

var host = builder.Build();
host.Run();
