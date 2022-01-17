using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace WinterTask.Clients.DiscordClient
{
    public class DiscordClient : DiscordSocketClient, IClient
    {
        private const string Token = "NjM2MTc4MTkzMDE5MzA1OTk1.Xa71HA.PczBWKnKTbIx9IQH4_yZjguS-wg";

        public async void LaunchClient()
        {
            base.Log += Log;
            MessageReceived += OnMessageReceived;
            await LoginAsync(TokenType.Bot, Token);
            await StartAsync();
        }

        public void ShutdownClient()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePoll(string chatId)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetPollUsers(int pollMessageId, string chatId)
        {
            throw new NotImplementedException();
        }

        public Task ReplyMessage(string chatId, string message)
        {
            throw new NotImplementedException();
        }


        private Task OnMessageReceived(SocketMessage socketMessage)
        {
            Console.WriteLine($"Got message '{socketMessage.Content}' from '{socketMessage.Author}'");

            if (!ShouldContinue(socketMessage))
                return Task.CompletedTask;

            ReplyMessage(socketMessage.Channel.Id.ToString(), socketMessage.Content);
            return Task.CompletedTask;
        }

        private new Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private bool ShouldContinue(SocketMessage socketMessage)
        {
            return socketMessage.Content is not null &&
                   socketMessage.Content.Length > 0;
        }
    }
}