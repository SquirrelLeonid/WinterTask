using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BotGames.Thousand;

namespace WinterTask.ChatBot
{
    public class BotMessageMaker
    {
        public BotMessage GetMessage(BotReply reply)
        {
            if (!reply.ReplyTypes.Any())
                return new BotMessage("I don't understand!", GetAvailableCommands(reply));
            var textFragments = new List<string>();
            foreach (var type in reply.ReplyTypes) textFragments.Add(GetReplyFragment(type, reply));

            var text = string.Join(Environment.NewLine, textFragments);
            return new BotMessage(text, GetAvailableCommands(reply));
        }

        public string GetReplyFragment(ReplyType replyType, BotReply reply)
        {
            switch (replyType)
            {
                case ReplyType.Help:
                    return GetHelpReply();
                case ReplyType.Start:
                    return "Let's start new game";
                case ReplyType.Poll:
                    return "Who will play?";
                case ReplyType.Roll:
                    return GetRollReply(reply.RollInfo);
                case ReplyType.ScoreBoard:
                    return reply.Board;
                case ReplyType.TurnEnd:
                    return "You finished turn. Your scores per turn: " + reply.Scores;
                case ReplyType.GameEnd:
                    return "Game is end";
                default:
                    return "I don't understand!";
            }
        }

        public string GetHelpReply()
        {
            var builder = new StringBuilder();
            builder.AppendLine("/help - to see this message");
            builder.AppendLine("/start - to start new game");
            builder.AppendLine("/board - to see the scoreboard");
            builder.AppendLine("/roll - to roll dices");
            builder.AppendLine("/end_turn - to end turn");
            builder.AppendLine("/end_game - to end game");
            return builder.ToString();
        }

        public string GetRollReply(RollInfo rollInfo)
        {
            var builder = new StringBuilder();
            builder.AppendLine("You roll next dices: " + string.Join(" ", rollInfo.DiceRolls));
            if (rollInfo.LeftDices > 0)
            {
                builder.AppendLine("Your scores per roll: " + rollInfo.RollScores);
                builder.AppendLine("You can reroll next dices count: " + rollInfo.LeftDices);
                builder.AppendLine("Your scores per turn: " + rollInfo.TurnScores);
            }
            else
            {
                builder.AppendLine("Your scores are fired");
            }

            return builder.ToString();
        }

        public Dictionary<string, string> GetAvailableCommands(BotReply reply)
        {
            var availableCommands = new Dictionary<string, string>();
            foreach (var command in reply.AvailableCommands) availableCommands["/" + command] = command;

            return availableCommands;
        }
    }
}