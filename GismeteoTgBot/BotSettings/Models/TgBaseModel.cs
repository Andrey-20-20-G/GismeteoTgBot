using GismeteoTgBot.BotSettings.TgHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace GismeteoTgBot.BotSettings.Models
{
    public class TgBaseModel : BackgroundService
    {
        [JsonPropertyName("TgBotToken")]
        private static readonly string _configuration;

        private readonly ITelegramBotClient _botClient = 
            new TelegramBotClient(""); //_configuration

        private ReceiverOptions _receiverOptions;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[]
                    {
                        UpdateType.Message,
                    },
                ThrowPendingUpdates = true,
            };

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(
                UpdateHandlerModel.UpdateHandler, ErrorHandlerModel.ErrorHandler,
                _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync();
            Console.WriteLine($"{me.FirstName} запущен!");

            await Task.Delay(-1);
        }
    }
}
