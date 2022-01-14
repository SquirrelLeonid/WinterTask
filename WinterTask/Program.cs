using System;
using Telegram.Bot;
using WinterTask.TelegramBot;

namespace WinterTask
{
    internal class Program
    {
        private const string Token = "5030819786:AAFQCHcxdIxcr1c3ihCfdwSaQn_zBFOKiY0";

        private static void Main(string[] args)
        {
            var telegramBot = TelegramBotFactory.CreateTelegramBot();

            var me = telegramBot.GetMeAsync().Result;

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
        }
    }
}