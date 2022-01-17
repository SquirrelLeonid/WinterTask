using System;
using WinterTask.Clients;

namespace WinterTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var telegramBot = BotClientFactory.GetBotClient(ClientType.Telegram);
            var discordBot = BotClientFactory.GetBotClient(ClientType.Discord);
            telegramBot.LaunchClient();
            //discordBot.LaunchClient();

            Console.ReadLine();
        }
    }
}