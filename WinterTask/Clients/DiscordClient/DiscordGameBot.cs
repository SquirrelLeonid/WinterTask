﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace WinterTask.Clients.DiscordClient
{
    public class DiscordGameBot : DiscordSocketClient, IClient
    {
        private const string Token = "NjM2MTc4MTkzMDE5MzA1OTk1.Xa71HA.PczBWKnKTbIx9IQH4_yZjguS-wg";

        public void ReplyToMessage(string id, string message)
        {
            throw new NotImplementedException();
        }

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

        private new Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}