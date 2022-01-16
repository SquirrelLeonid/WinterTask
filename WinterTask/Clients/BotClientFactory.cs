using System;

namespace WinterTask.Clients
{
    public class BotClientFactory
    {
        public static IClient GetBotClient(BotType clientType)
        {
            return clientType switch
            {
                BotType.Telegram => new TelegramClient.TelegramClient(),
                BotType.Discord => new DiscordClient.DiscordClient(),
                _ => throw new ArgumentException($"Unknown bot type {clientType}")
            };
        }
    }
}