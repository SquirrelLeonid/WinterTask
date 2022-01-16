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
            await LoginAsync(TokenType.Bot, Token);
            await StartAsync();
        }

        public void ShutdownClient()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreatePoll(long chatId)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetPollUsers(int pollMessageId, long chatId)
        {
            throw new NotImplementedException();
        }

        Task IClient.ReplyMessage(long chatId, string message)
        {
            throw new NotImplementedException();
        }

        private new Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public void ReplyMessage(long chatId, string message)
        {
            throw new NotImplementedException();
        }
    }
}