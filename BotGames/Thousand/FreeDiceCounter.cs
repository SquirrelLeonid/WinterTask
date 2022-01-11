using System.Linq;

namespace BotGames.Thousand
{
    public class FreeDiceCounter
    {
        public static int CountFreeDices(int[] diceRoll)
        {
            var diceCounts = diceRoll
                .GroupBy(dice => dice)
                .ToDictionary(group => group.Key, group => group.Count());

            if (diceCounts.Count == 5)
                return 5;

            var count = diceCounts
                .Where(pair => pair.Value < 3
                               && pair.Key != 1
                               && pair.Key != 5)
                .Select(pair => pair.Value)
                .Sum();

            return count == 0 ? 5 : count;
        }
    }
}