using System.Collections.Generic;
using System.Linq;

namespace BotGames.Thousand
{
    public class DiceRollScoreCounter
    {
        public static int CountScore(IEnumerable<int> diceRoll)
        {
            var diceCounts = diceRoll
                .GroupBy(dice => dice)
                .ToDictionary(group => group.Key, group => group.Count());

            if (diceCounts.Count == 5)
                return diceCounts.ContainsKey(1) ? 125 : 250;

            return CountNonSequenceCombination(diceCounts);
        }

        private static int CountNonSequenceCombination(Dictionary<int, int> diceCounts)
        {
            var totalScores = 0;

            foreach (var pair in diceCounts)
            {
                var diceScore = pair.Key == 1 ? 10 : pair.Key;
                var diceCount = pair.Value;
                switch (diceCount)
                {
                    case 1:
                    case 2:
                        if (diceScore == 10 || diceScore == 5)
                            totalScores += diceScore * diceCount;
                        break;
                    case 3:
                        totalScores += diceScore * 10;
                        break;
                    case 4:
                        totalScores += diceScore * 20;
                        break;
                    case 5:
                        totalScores += diceScore * 100;
                        break;
                }
            }

            return totalScores;
        }
    }
}