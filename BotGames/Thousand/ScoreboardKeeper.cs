using System.Collections.Generic;
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
            var playerNames = new List<string>();
            var values = new List<object>();
            foreach (var pair in Scoreboard)
            {
                playerNames.Add(pair.Key.PlayerName);
                values.Add(pair.Value);
            }

            var table = new ConsoleTable(playerNames.ToArray());
            table.AddRow(values.ToArray());

            return table.ToMinimalString();
        }
    }
}