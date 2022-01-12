using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace BotGames.Thousand
{
    public class ScoreboardKeeper
    {
        public readonly Dictionary<Player, int> Scoreboard;

        public ScoreboardKeeper(IEnumerable<Player> players)
        {
            Scoreboard = new Dictionary<Player, int>();

            foreach (var player in players) Scoreboard.Add(player, 0);
        }

        public string GetStringScoreBoard()
        {
            var playerNames = Scoreboard.Keys.Select(player => player.PlayerName).ToArray();
            var table = new ConsoleTable(playerNames);

            table.AddRow(Scoreboard.Values);

            return table.ToMinimalString();
        }
    }
}