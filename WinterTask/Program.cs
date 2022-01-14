using System;
using System.Threading;
using Telegram.Bot;
using WinterTask.Bots.DiscordBot;
using WinterTask.Bots.TelegramBot;

namespace WinterTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var telegramBot = TelegramBotFactory.CreateTelegramBot();
            var discordBot = DiscordBotFactory.CreateDiscordBot();

            Thread.Sleep(2000);

            var me = telegramBot.GetMeAsync().Result;
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }
    }
}