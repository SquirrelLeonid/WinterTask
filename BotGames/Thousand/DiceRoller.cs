using System;
using System.Collections.Generic;

namespace BotGames.Thousand
{
    public class DiceRoller
    {
        private readonly Random random;

        public DiceRoller()
        {
            random = new Random();
        }

        public int RollNextDice()
        {
            return random.Next(1, 6);
        }

        public IEnumerable<int> RollNextDices(int diceNumber)
        {
            for (var i = 0; i < diceNumber; i++)
                yield return RollNextDice();
        }
    }
}