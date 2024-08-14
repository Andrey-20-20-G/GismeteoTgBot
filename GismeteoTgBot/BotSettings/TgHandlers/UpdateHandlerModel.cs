using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using GismeteoTgBot.WeatherService.Parser;

namespace GismeteoTgBot.BotSettings.TgHandlers
{
    public class UpdateHandlerModel
    {
        private static readonly WeatherParser _weatherParserAsync = new();
        public static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            var chat = message.Chat;
                            await botClient.SendTextMessageAsync(
                                chat.Id,
                                await _weatherParserAsync.GetWeatherListForDay(), // отправляем то, что написал пользователь
                                replyToMessageId: message.MessageId // по желанию можем поставить этот параметр, отвечающий за "ответ" на сообщение
                                );
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
