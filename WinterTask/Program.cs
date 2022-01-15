using System;
using Telegram.Bot;
using WinterTask.Bots.TelegramBot;

namespace WinterTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var telegramBot = TelegramBotFactory.CreateTelegramBot();
            //var discordBot = DiscordBotFactory.CreateDiscordBot();

            var me = telegramBot.GetMeAsync().Result;

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }
    }
}