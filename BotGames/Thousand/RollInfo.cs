namespace BotGames.Thousand
{
    public class RollInfo
    {
        public RollInfo(int[] diceRolls, int rollScores, int leftDices)
        {
            DiceRolls = diceRolls;
            RollScores = rollScores;
            LeftDices = leftDices;
        }

        public int RollScores { get; }
        public int LeftDices { get; }
        public int[] DiceRolls { get; }
        public int TurnScores { get; set; }
    }
}