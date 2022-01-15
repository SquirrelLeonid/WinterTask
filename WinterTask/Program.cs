using System;
using WinterTask.Clients;

namespace WinterTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var telegramBot = BotClientFactory.GetBotClient(BotType.Telegram);
            var discordBot = BotClientFactory.GetBotClient(BotType.Discord);
            telegramBot.LaunchClient();
            discordBot.LaunchClient();

            Console.ReadLine();
        }
    }
}