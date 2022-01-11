using BotGames.Thousand;
using FluentAssertions;
using NUnit.Framework;

namespace BotGamesTests.ThousandTests
{
    public class DiceRollScoreCounterTests
    {
        [TestCase(new[] { 1, 2, 3, 4, 4 }, 10)]
        [TestCase(new[] { 5, 2, 2, 3, 3 }, 5)]
        public void ScoreCounter_ShouldCountSingleFiveOrTen(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 1, 1, 3, 4, 4 }, 20)]
        [TestCase(new[] { 5, 5, 2, 3, 3 }, 10)]
        [TestCase(new[] { 1, 1, 5, 5, 4 }, 30)]
        public void ScoreCounter_ShouldCountMultipleFiveOrTen(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 2, 2, 3, 3, 4 })]
        public void ScoreCounter_ShouldCountZeroScores_WhenNoScores(int[] diceRoll)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(0);
        }

        [TestCase(new[] { 1, 1, 1, 2, 2 }, 100)]
        [TestCase(new[] { 2, 2, 2, 3, 3 }, 20)]
        [TestCase(new[] { 3, 3, 3, 2, 2 }, 30)]
        [TestCase(new[] { 4, 4, 4, 3, 3 }, 40)]
        [TestCase(new[] { 5, 5, 5, 3, 3 }, 50)]
        [TestCase(new[] { 6, 6, 6, 3, 3 }, 60)]
        public void ScoreCounter_ShouldCount_CombinationOfThree(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 1, 1, 1, 1, 2 }, 200)]
        [TestCase(new[] { 2, 2, 2, 2, 3 }, 40)]
        [TestCase(new[] { 3, 3, 3, 3, 2 }, 60)]
        [TestCase(new[] { 4, 4, 4, 4, 3 }, 80)]
        [TestCase(new[] { 5, 5, 5, 5, 3 }, 100)]
        [TestCase(new[] { 6, 6, 6, 6, 3 }, 120)]
        public void ScoreCounter_ShouldCount_CombinationOfFour(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 1, 1, 1, 1, 1 }, 1000)]
        [TestCase(new[] { 2, 2, 2, 2, 2 }, 200)]
        [TestCase(new[] { 3, 3, 3, 3, 3 }, 300)]
        [TestCase(new[] { 4, 4, 4, 4, 4 }, 400)]
        [TestCase(new[] { 5, 5, 5, 5, 5 }, 500)]
        [TestCase(new[] { 6, 6, 6, 6, 6 }, 600)]
        public void ScoreCounter_ShouldCount_CombinationOfFive(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 125)]
        [TestCase(new[] { 2, 3, 4, 5, 6 }, 250)]
        public void ScoreCounter_ShouldCount_CombinationOfSequence(int[] diceRoll, int expectedScores)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(expectedScores);
        }

        [TestCase(new[] { 2, 2, 3, 3, 4 })]
        public void ScoreCount_ShouldCount_ZeroScores(int[] diceRoll)
        {
            var actualScores = DiceRollScoreCounter.CountScore(diceRoll);

            actualScores.Should().Be(0);
        }
    }
}