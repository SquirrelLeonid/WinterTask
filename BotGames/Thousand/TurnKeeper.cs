using System.Collections.Generic;
using System.Linq;

namespace BotGames.Thousand
{
    public class TurnKeeper
    {
        private readonly DiceRoller diceRoller;
        private readonly List<RollInfo> diceRolls;
        public int Scores;

        public TurnKeeper(Player player)
        {
            diceRoller = new DiceRoller();
            diceRolls = new List<RollInfo>();
            Player = player;
        }

        public Player Player { get; }

        private int FreeDices => diceRolls.LastOrDefault()?.LeftDices ?? 5;

        public RollInfo MakeRoll()
        {
            var diceRoll = diceRoller.RollNextDices(FreeDices).ToArray();
            var rollInfo = DiceRollAnalyzer.AnalyzeDiceRoll(diceRoll);

            diceRolls.Add(rollInfo);
            if (!IsRollEffective(rollInfo))
                Scores = 0;
            else
                Scores += rollInfo.Scores;

            return rollInfo;
        }

        public bool IsLastRollWasEffective()
        {
            return IsRollEffective(diceRolls.Last());
        }

        private bool IsRollEffective(RollInfo rollInfo)
        {
            return rollInfo.Scores > 0;
        }
    }
}