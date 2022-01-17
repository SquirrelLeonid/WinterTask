using System.Collections.Generic;
using BotGames.Thousand;

namespace WinterTask.ChatBot
{
    public class BotReply
    {
        public BotReply(
            List<ReplyType> replyTypes,
            string board,
            RollInfo rollInfo,
            int scores,
            string[] availableCommands)
        {
            ReplyTypes = replyTypes;
            Board = board;
            RollInfo = rollInfo;
            RollInfo = rollInfo;
            Scores = scores;
            AvailableCommands = availableCommands;
        }

        public string Board { get; }
        public List<ReplyType> ReplyTypes { get; }
        public RollInfo RollInfo { get; }
        public int Scores { get; }
        public string[] AvailableCommands { get; }
    }
}