namespace BotGames.Thousand
{
    public class RollInfo
    {
        public RollInfo(int[] diceRolls, int scores, int leftDices)
        {
            DiceRolls = diceRolls;
            Scores = scores;
            LeftDices = leftDices;
        }

        public int Scores { get; }
        public int LeftDices { get; }
        public int[] DiceRolls { get; }
    }
}