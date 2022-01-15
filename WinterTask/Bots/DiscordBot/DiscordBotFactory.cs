using Discord;

namespace WinterTask.Bots.DiscordBot
{
    public class DiscordBotFactory
    {
        public static IDiscordClient CreateDiscordBot()
        {
            var bot = new DiscordGameBotHandler();
            bot.LaunchBot();
            return bot;
        }
    }
}