namespace BotGames.Thousand
{
    public class DiceRollAnalyzer
    {
        public static RollInfo AnalyzeDiceRoll(int[] diceRoll)
        {
            var scores = DiceRollScoreCounter.CountScore(diceRoll);
            var dicesLeft = FreeDiceCounter.CountFreeDices(diceRoll);

            return new RollInfo(diceRoll, scores, dicesLeft);
        }
    }
}