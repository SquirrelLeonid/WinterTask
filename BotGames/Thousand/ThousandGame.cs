using System.Collections.Generic;
using System.Linq;

namespace BotGames.Thousand
{
    public class ThousandGame
    {
        private readonly Dictionary<Player, int> bolts;
        private readonly List<Player> players;
        private readonly ScoreboardKeeper scoreboardKeeper;
        private int currentPlayerPointer;
        private TurnKeeper turnKeeper;

        public ThousandGame(IEnumerable<Player> players)
        {
            currentPlayerPointer = 0;
            this.players = new List<Player>(players);
            scoreboardKeeper = new ScoreboardKeeper(this.players);
            turnKeeper = new TurnKeeper(this.players[0]);
            bolts = new Dictionary<Player, int>();
            foreach (var player in this.players) bolts[player] = 0;
        }

        private Player CurrentPlayer => players[currentPlayerPointer];

        public bool IsEnd()
        {
            return scoreboardKeeper.Scoreboard.Any(pair => pair.Value >= 1000);
        }

        public RollInfo MakeRoll()
        {
            return turnKeeper.MakeRoll();
        }

        public bool CanRoll()
        {
            return !IsEnd() && turnKeeper.IsLastRollWasEffective();
        }

        public int FinishTurn()
        {
            if (turnKeeper.Scores > 0)
            {
                bolts[CurrentPlayer] = 0;
                scoreboardKeeper.Scoreboard[CurrentPlayer] += turnKeeper.Scores;
            }
            else
            {
                bolts[CurrentPlayer] += 1;
                if (bolts[CurrentPlayer] == 3)
                {
                    scoreboardKeeper.Scoreboard[CurrentPlayer] -= 50;
                    bolts[CurrentPlayer] = 0;
                }
            }

            if (CheckDumpTrack())
            {
                scoreboardKeeper.Scoreboard[players[currentPlayerPointer]] = 0;
            }

            else if (CheckPlayerInHole())
            {
                var scores = scoreboardKeeper.Scoreboard[players[currentPlayerPointer]];
                if (scores % 100 == 2 || scores % 100 == 6)
                {
                    scoreboardKeeper.Scoreboard[players[currentPlayerPointer]] = scores - scores % 100;
                    turnKeeper.Scores = 0;
                }
            }

            var scoresPerTurn = turnKeeper.Scores;
            currentPlayerPointer += 1;
            currentPlayerPointer %= players.Count;
            turnKeeper = new TurnKeeper(CurrentPlayer);
            return scoresPerTurn;
        }

        public string GetScoreBoard()
        {
            return scoreboardKeeper.GetStringScoreBoard();
        }

        private bool CheckDumpTrack()
        {
            return scoreboardKeeper.Scoreboard[CurrentPlayer] == 555;
        }

        private bool CheckPlayerInHole()
        {
            var scores = scoreboardKeeper.Scoreboard[CurrentPlayer];
            return scores >= 200 && scores < 300 || scores >= 600 && scores < 700;
        }
    }
}