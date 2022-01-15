using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WinterTask.Clients.TelegramClient
{
    public class TelegramClient : TelegramBotClient, IClient
    {
        private const string Token = "5030819786:AAFQCHcxdIxcr1c3ihCfdwSaQn_zBFOKiY0";
        private readonly CancellationTokenSource cts;
        private readonly Dictionary<string, List<User>> polls;
        private readonly ReceiverOptions receiverOptions;
        private readonly HashSet<Timer> timers;

        public TelegramClient() : base(Token)
        {
            cts = new CancellationTokenSource();
            receiverOptions = new ReceiverOptions();
            timers = new HashSet<Timer>();
            polls = new Dictionary<string, List<User>>();
        }

        public void ReplyToMessage(string id, string message)
        {
            throw new NotImplementedException();
        }

        public void LaunchClient()
        {
            this.StartReceiving(
                OnUpdateAsync,
                OnErrorAsync,
                receiverOptions,
                cts.Token);
        }

        public void ShutdownClient()
        {
            cts.Cancel();
        }

        public async void CreateStartGameTask(long chatId)
        {
            var pollMessage = await this.SendPollAsync(
                chatId,
                "Do you want to play?",
                new[]
                {
                    "Yes",
                    "YESSSS"
                },
                false,
                cancellationToken: cts.Token);
        }

        private async Task OnUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.PollAnswer)
            {
                var answer = update.PollAnswer;
                if (!polls.ContainsKey(answer.PollId))
                    polls[answer.PollId] = new List<User>();

                polls[answer.PollId].Add(answer.User);

                Console.WriteLine(answer.User.Username);
            }

            if (update.Type != UpdateType.Message && update.Type != UpdateType.ChannelPost)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

            CreateStartGameTask(chatId);

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "You said:\n" + messageText,
                cancellationToken: cancellationToken);
        }

        private Task OnErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

        public async void CreateGame(Message pollMessage)
        {
            var poll = await this.StopPollAsync(
                pollMessage.Chat.Id,
                pollMessage.MessageId,
                cancellationToken: cts.Token);
        }
    }
}