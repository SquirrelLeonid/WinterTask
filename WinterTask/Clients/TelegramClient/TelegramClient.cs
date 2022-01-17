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
using Telegram.Bot.Types.ReplyMarkups;
using WinterTask.ChatBot;

namespace WinterTask.Clients.TelegramClient
{
    public class TelegramClient : TelegramBotClient, IClient
    {
        private const string Token = "5030819786:AAFQCHcxdIxcr1c3ihCfdwSaQn_zBFOKiY0";
        private readonly BotMessageMaker botMessageMaker;
        private readonly Dictionary<string, IChatBot> bots;
        private readonly CancellationTokenSource cts = new();
        private readonly Dictionary<string, List<User>> polls;
        private readonly ReceiverOptions receiverOptions = new();

        public TelegramClient() : base(Token)
        {
            receiverOptions.ThrowPendingUpdates = true;
            bots = new Dictionary<string, IChatBot>();
            polls = new Dictionary<string, List<User>>();
            botMessageMaker = new BotMessageMaker();
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

        public async Task ReplyMessage(string chatId, string message)
        {
            if (!bots.ContainsKey(chatId))
                bots[chatId] = ChatBotFactory.CreateChatBot(chatId);

            var botReply = bots[chatId].ReplyToMessage(this, message);
            var botMessage = botMessageMaker.GetMessage(botReply);
            await SendTextMessage(chatId, botMessage.Text, botMessage.AvailableOperations);
        }

        public async Task<User[]> GetPollUsers(int pollMessageId, string chatId)
        {
            var poll = await this.StopPollAsync(
                chatId,
                pollMessageId,
                cancellationToken: cts.Token);
            var users = polls[poll.Id];
            polls.Remove(poll.Id);
            return users.ToArray();
        }

        public async Task<int> CreatePoll(string chatId)
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
            return pollMessage.MessageId;
        }

        private async Task OnUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (!ShouldContinue(update))
                return;

            var chatId = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;
            var messageText = update.Message?.Text ?? update.CallbackQuery?.Data;

            if (update.Type == UpdateType.Message || update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine($"Received a '{messageText}' message in chat {chatId.Value}.");
                await ReplyMessage(chatId.Value.ToString(), messageText);
            }

            if (update.Type == UpdateType.PollAnswer && update.PollAnswer is not null)
            {
                var answer = update.PollAnswer;
                if (!polls.ContainsKey(answer.PollId))
                    polls[answer.PollId] = new List<User>();

                polls[answer.PollId].Add(new User(answer.User.Id, answer.User.Username));

                Console.WriteLine(answer.User.Username + $" answered '{answer.OptionIds.First()}'");
            }
        }

        private bool ShouldContinue(Update update)
        {
            if (update.Type == UpdateType.Message
                && update.Message is not null
                && update.Message.Type == MessageType.Text)
                return true;

            if (update.Type == UpdateType.PollAnswer)
                return true;

            if (update.Type == UpdateType.CallbackQuery)
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

        private async Task SendTextMessage(string chatId, string text, Dictionary<string, string> availableCommands)
        {
            var rows = new List<List<InlineKeyboardButton>>();
            List<InlineKeyboardButton> row = null;
            var i = 0;
            foreach (var pair in availableCommands)
            {
                if (i % 2 == 0)
                {
                    if (row != null)
                        rows.Add(row);
                    row = new List<InlineKeyboardButton>();
                }

                row?.Add(InlineKeyboardButton.WithCallbackData(pair.Value, pair.Key));
                i++;
            }

            if (row != null && row.Count != 0)
                rows.Add(row);
            var markup = new InlineKeyboardMarkup(rows);

            await this.SendTextMessageAsync(
                chatId,
                text,
                replyMarkup: markup,
                cancellationToken: cts.Token);
        }
    }
}