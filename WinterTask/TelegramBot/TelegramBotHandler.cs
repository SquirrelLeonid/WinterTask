using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WinterTask.TelegramBot
{
    public class TelegramBotHandler : TelegramBotClient, IBot, IDisposable
    {
        private const string Token = "5030819786:AAFQCHcxdIxcr1c3ihCfdwSaQn_zBFOKiY0";
        private readonly CancellationTokenSource cts;
        private readonly ReceiverOptions receiverOptions;

        public TelegramBotHandler() : base(Token)
        {
            cts = new CancellationTokenSource();
            receiverOptions = new ReceiverOptions();
        }

        public void ReplyToMessage(string message, string id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            cts.Cancel();
        }

        public void LaunchBot()
        {
            this.StartReceiving(
                OnUpdateAsync,
                OnErrorAsync,
                receiverOptions,
                cts.Token);
        }

        private async Task OnUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;

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
    }
}