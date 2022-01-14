using Discord;

namespace WinterTask.Bots.DiscordBot
{
    public class DiscordBotFactory
    {
        public static IDiscordClient CreateDiscordBot()
        {
            var bot = new DiscordBotHandler();
            bot.LaunchBot();
            return bot;
        }
    }
}