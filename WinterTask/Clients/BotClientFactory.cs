using System;

namespace WinterTask.Clients
{
    public class BotClientFactory
    {
        public static IClient GetBotClient(ClientType clientType)
        {
            return clientType switch
            {
                ClientType.Telegram => new TelegramClient.TelegramClient(),
                ClientType.Discord => new DiscordClient.DiscordClient(),
                _ => throw new ArgumentException($"Unknown bot type {clientType}")
            };
        }
    }
}