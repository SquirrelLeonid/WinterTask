namespace WinterTask.Bots.TelegramBot
{
    public class TelegramBotFactory
    {
        public static TelegramGameBotHandler CreateTelegramBot()
        {
            var bot = new TelegramGameBotHandler();
            bot.LaunchBot();
            return bot;
        }
    }
}