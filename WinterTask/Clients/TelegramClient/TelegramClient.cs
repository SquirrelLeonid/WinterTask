using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Dictionary<long, IChatBot> bots;
        private readonly CancellationTokenSource cts = new();
        private readonly Dictionary<string, List<User>> polls;
        private readonly ReceiverOptions receiverOptions = new();

        public TelegramClient() : base(Token)
        {
            receiverOptions.ThrowPendingUpdates = true;
            bots = new Dictionary<long, IChatBot>();
            polls = new Dictionary<string, List<User>>();
        }

        public void LaunchClient()
        {
            this.StartReceiving(
                OnUpdateAsync,
                OnErrorAsync,
                receiverOptions,
                cts.Token);

            Console.WriteLine("Telegram bot is launched!");
        }

        public void ShutdownClient()
        {
            cts.Cancel();
        }

        public void SendMessage(string chatId, string message)
        {
            throw new NotImplementedException();
        }

        public async void CreateStartGameTask(long chatId)
        {
            var question = "Do you want to play?";
            var options = new[]
            {
                "Yes",
                "YES"
            };

            var pollMessage = await this.SendPollAsync(
                chatId,
                question,
                options,
                allowsMultipleAnswers: false,
                isAnonymous: false,
                cancellationToken: cts.Token);
        }

        private async Task OnUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (!ShouldContinue(update))
                return;

            var chatId = update.Message?.Chat.Id;
            var messageText = update.Message?.Text;

            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine($"Received a '{messageText}' message in chat {chatId.Value}.");
                CreateStartGameTask(chatId.Value);

                if (!bots.ContainsKey(chatId.Value))
                    bots[chatId.Value] = ChatBotFactory.CreateChatBot();

                bots[chatId.Value].ReplyToMessage(this, messageText);
            }

            if (update.Type == UpdateType.PollAnswer && update.PollAnswer is not null)
            {
                var answer = update.PollAnswer;
                if (!polls.ContainsKey(answer.PollId))
                    polls[answer.PollId] = new List<User>();

                polls[answer.PollId].Add(answer.User);

                Console.WriteLine(answer.User.Username + $" answered '{answer.OptionIds.First()}'");
            }


            //var sentMessage = await botClient.SendTextMessageAsync(
            //    chatId.Value,
            //    "You said:\n" + messageText,
            //    cancellationToken: cancellationToken);
        }

        private bool ShouldContinue(Update update)
        {
            if (update.Type == UpdateType.Message
                && update.Message is not null
                && update.Message.Type == MessageType.Text)
                return true;

            if (update.Type == UpdateType.PollAnswer)
                return true;

            return false;
        }

        private Task OnErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => "Telegram API Error:\n" +
                       $"[{apiRequestException.ErrorCode}]\n" +
                       $"{apiRequestException.Message}",
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