using System.Linq;
using System.Threading.Tasks;
using BotGames.Thousand;
using WinterTask.Clients;

namespace WinterTask
{
    public class ChatBot : IChatBot
    {
        private readonly long chatId;
        private ThousandGame game;

        public ChatBot(long id)
        {
            chatId = id;
        }

        public BotReply ReplyToMessage(IClient botClient, string message)
        {
            var replyBuilder = new BotReplyBuilder();
            var messageSplit = message.Split(' ');
            var command = messageSplit[0].Split('@')[0];
            var arguments = messageSplit.Skip(1).ToArray();
            switch (command)
            {
                case "/help":
                    ReplyHelp(replyBuilder);
                    break;
                case "/start":
                    ReplyStartGame(botClient, replyBuilder, arguments);
                    break;
                case "/end_game":
                    ReplyEndGame(replyBuilder);
                    break;
                case "/end_turn":
                    ReplyEndTurn(replyBuilder);
                    break;
                case "/board":
                    ReplyBoard(replyBuilder);
                    break;
                case "/roll":
                    ReplyRoll(replyBuilder);
                    break;
            }

            return replyBuilder.BuildReply();
        }

        public void ReplyStartGame(
            IClient botClient,
            BotReplyBuilder botReplyBuilder,
            string[] arguments)
        {
            if (arguments.Any())
            {
                game = new ThousandGame(arguments.Select(name => new Player(name)));
                botReplyBuilder.AddReplyType(ReplyType.Start);
                return;
            }

            botReplyBuilder.AddReplyType(ReplyType.Poll);
            var pollMessageId = botClient.CreatePoll(chatId).Result;
            Task.Delay(1000 * 5).ContinueWith(_ =>
            {
                var users = botClient.GetPollUsers(pollMessageId, chatId).Result;
                botClient.ReplyMessage(
                    chatId,
                    "/start " + string.Join(" ", users.Select(user => user.UserName)));
            });
        }

        public void ReplyHelp(BotReplyBuilder replyBuilder)
        {
            replyBuilder.AddReplyType(ReplyType.Help);
        }

        public void ReplyEndGame(BotReplyBuilder replyBuilder)
        {
            replyBuilder.AddReplyType(ReplyType.GameEnd);
            game = null;
            ReplyBoard(replyBuilder);
        }

        public void ReplyEndTurn(BotReplyBuilder replyBuilder)
        {
            var scores = game.FinishTurn();
            replyBuilder
                .AddReplyType(ReplyType.TurnEnd)
                .SetScoresPerTurn(scores);
            if (game.IsEnd())
                ReplyEndGame(replyBuilder);
        }

        public void ReplyBoard(BotReplyBuilder replyBuilder)
        {
            replyBuilder
                .AddReplyType(ReplyType.ScoreBoard)
                .SetBoard(game.GetScoreBoard());
        }

        public void ReplyRoll(BotReplyBuilder replyBuilder)
        {
            var rollInfo = game.MakeRoll();
            replyBuilder
                .AddReplyType(ReplyType.Roll)
                .SetRollInfo(rollInfo);
            if (rollInfo.LeftDices == 0)
                ReplyEndTurn(replyBuilder);
        }
    }
}