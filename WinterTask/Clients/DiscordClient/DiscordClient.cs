using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using WinterTask.ChatBot;

namespace WinterTask.Clients.DiscordClient
{
    public class DiscordClient : DiscordSocketClient, IClient
    {
        private const string Token = "NjM2MTc4MTkzMDE5MzA1OTk1.Xa71HA.PczBWKnKTbIx9IQH4_yZjguS-wg";
        private readonly BotMessageMaker botMessageMaker;
        private readonly Dictionary<string, IChatBot> bots;
        private readonly Dictionary<string, List<User>> polls;

        public DiscordClient()
        {
            bots = new Dictionary<string, IChatBot>();
            botMessageMaker = new BotMessageMaker();
            polls = new Dictionary<string, List<User>>();
        }

        public async void LaunchClient()
        {
            base.Log += Log;
            MessageReceived += OnMessageReceived;
            ReactionAdded += OnReactionReceived;
            ButtonExecuted += OnButtonClicked;
            await LoginAsync(TokenType.Bot, Token);
            await StartAsync();
        }

        public void ShutdownClient()
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreatePoll(string chatId)
        {
            var channel = GetChannel(ulong.Parse(chatId)) as IMessageChannel;

            var sendingMessage = await channel.SendMessageAsync("Who will play?");
            await sendingMessage.AddReactionAsync(new Emoji("🎲"));
            return sendingMessage.Id.ToString();
        }

        public async Task<User[]> GetPollUsers(string pollMessageId, string chatId)
        {
            var channel = await GetChannelAsync(ulong.Parse(chatId)) as IMessageChannel;
            await channel.DeleteMessageAsync(ulong.Parse(pollMessageId));

            var users = polls[pollMessageId];
            polls.Remove(pollMessageId);

            return users.ToArray();
        }

        public async Task ReplyMessage(string chatId, string message)
        {
            if (!bots.ContainsKey(chatId))
                bots[chatId] = ChatBotFactory.CreateChatBot(chatId);

            var botReply = bots[chatId].ReplyToMessage(this, message);
            var botMessage = botMessageMaker.GetMessage(botReply);
            await SendTextMessage(
                chatId,
                botMessage.Text,
                botMessage.AvailableOperations);
        }

        private async Task OnButtonClicked(SocketMessageComponent button)
        {
            await button.RespondAsync("Got it!");
            await ReplyMessage(button.Channel.Id.ToString(), button.Data.CustomId);
        }

        private async Task OnReactionReceived(
            Cacheable<IUserMessage, ulong> reactedMessage,
            Cacheable<IMessageChannel, ulong> reactedChannel,
            SocketReaction socketReaction)
        {
            if (socketReaction.User.Value.IsBot)
                return;

            var channel = reactedChannel.Value;
            var message = await channel.GetMessageAsync(reactedMessage.Id);

            if (socketReaction.User.Value.IsBot)
                return;

            if (message.Content == "Who will play?" && socketReaction.Emote.Name == "🎲")
            {
                var pollMessageId = message.Id.ToString();

                if (!polls.ContainsKey(pollMessageId))
                    polls[pollMessageId] = new List<User>();

                polls[pollMessageId].Add(new User(
                    socketReaction.UserId.ToString(),
                    socketReaction.User.Value.Username));
            }
        }

        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            if (!ShouldContinue(socketMessage))
                return;

            Console.WriteLine($"Got message '{socketMessage.Content}' from '{socketMessage.Author}'");

            await ReplyMessage(
                socketMessage.Channel.Id.ToString(),
                socketMessage.Content);
        }

        private bool ShouldContinue(SocketMessage socketMessage)
        {
            if (socketMessage.Author.IsBot)
                return false;

            return socketMessage.Content is not null &&
                   socketMessage.Content.Length > 0;
        }

        private async Task SendTextMessage(
            string chatId,
            string text,
            Dictionary<string, string> availableCommands)
        {
            var buttonBuilder = new ComponentBuilder();
            foreach (var availableCommand in availableCommands)
                buttonBuilder.WithButton(availableCommand.Value, availableCommand.Key);

            var channel = GetChannel(ulong.Parse(chatId)) as IMessageChannel;

            await channel.SendMessageAsync(text, components: buttonBuilder.Build());
        }

        private new Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}