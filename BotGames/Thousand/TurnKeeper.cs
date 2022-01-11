using System.Collections.Generic;
using System.Linq;

namespace BotGames.Thousand
{
    public class TurnKeeper
    {
        private readonly DiceRoller diceRoller;
        private readonly List<RollInfo> diceRolls;

        public TurnKeeper()
        {
            diceRoller = new DiceRoller();
            diceRolls = new List<RollInfo>();
        }

        private int FreeDices => diceRolls.LastOrDefault()?.LeftDices ?? 5;

        public void MakeRoll()
        {
            var diceRoll = diceRoller.RollNextDices(FreeDices).ToArray();
            var rollInfo = DiceRollAnalyzer.AnalyzeDiceRoll(diceRoll);

            diceRolls.Add(rollInfo);
        }

        public bool CanRoll()
        {
            return diceRolls.Last().Scores > 0;
        }
    }
}