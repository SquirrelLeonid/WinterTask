using BotGames.Thousand;
using FluentAssertions;
using NUnit.Framework;

namespace BotGamesTests.ThousandTests
{
    public class FreeDiceCounterTests
    {
        [TestCase(new[] { 1, 2, 3, 4, 4 }, 4)]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 5)]
        [TestCase(new[] { 2, 3, 4, 5, 6 }, 5)]
        [TestCase(new[] { 1, 1, 1, 4, 4 }, 2)]
        [TestCase(new[] { 2, 2, 3, 4, 4 }, 5)]
        [TestCase(new[] { 1, 1, 1, 5, 5 }, 5)]
        public void DiceCounter_ShouldCountFreeDices(int[] diceRoll, int expectedDiceCount)
        {
            var actualDiceCount = FreeDiceCounter.CountFreeDices(diceRoll);

            actualDiceCount.Should().Be(expectedDiceCount);
        }
    }
}