using System.Collections.Generic;
using BotGames.Thousand;

namespace WinterTask
{
    public class BotReply
    {
        public BotReply(List<ReplyType> replyTypes, string board, RollInfo rollInfo, int scores)
        {
            ReplyTypes = replyTypes;
            Board = board;
            RollInfo = rollInfo;
            RollInfo = rollInfo;
            Scores = scores;
        }

        public string Board { get; }
        public List<ReplyType> ReplyTypes { get; }
        public RollInfo RollInfo { get; }
        public int Scores { get; }
    }
}