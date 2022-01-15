using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace WinterTask.Bots.DiscordBot
{
    public class DiscordGameBotHandler : DiscordSocketClient, IGameBot
    {
        private const string Token = "NjM2MTc4MTkzMDE5MzA1OTk1.Xa71HA.PczBWKnKTbIx9IQH4_yZjguS-wg";

        public void ReplyToMessage(string message, string id)
        {
            throw new NotImplementedException();
        }

        public void CreateStartGameTask(long chatId)
        {
            throw new NotImplementedException();
        }

        public async void LaunchBot()
        {
            base.Log += Log;
            await LoginAsync(TokenType.Bot, Token);
            await StartAsync();
        }

        private new Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}