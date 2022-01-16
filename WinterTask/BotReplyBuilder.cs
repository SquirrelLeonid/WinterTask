﻿using System.Collections.Generic;
using BotGames.Thousand;

namespace WinterTask
{
    public class BotReplyBuilder
    {
        private readonly List<ReplyType> replyTypes;
        private RollInfo rollInfo;
        private string scoreBoard;
        private int scores;

        public BotReplyBuilder()
        {
            replyTypes = new List<ReplyType>();
        }

        public BotReplyBuilder AddReplyType(ReplyType replyType)
        {
            replyTypes.Add(replyType);
            return this;
        }

        public BotReplyBuilder SetBoard(string board)
        {
            scoreBoard = board;
            return this;
        }

        public BotReplyBuilder SetRollInfo(RollInfo info)
        {
            rollInfo = info;
            return this;
        }

        public BotReplyBuilder SetScoresPerTurn(int score)
        {
            scores = score;
            return this;
        }

        public BotReply BuildReply()
        {
            return new BotReply(replyTypes, scoreBoard, rollInfo, scores);
        }
    }
}