namespace BotGames.Thousand
{
    public class RollInfo
    {
        public RollInfo(int scores, int leftDices)
        {
            Scores = scores;
            LeftDices = leftDices;
        }

        public int Scores { get; }
        public int LeftDices { get; }
    }
}