using Telegram.Bot;

namespace WinterTask.TelegramBot
{
    public class TelegramBotFactory
    {
        public static ITelegramBotClient CreateTelegramBot()
        {
            var bot = new TelegramBotHandler();
            bot.LaunchBot();
            return bot;
        }
    }
}