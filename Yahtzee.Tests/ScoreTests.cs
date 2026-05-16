using NUnit.Framework;
using Yahtzee.Logic;

namespace Yahtzee.Tests;

public class ScoreTests
{
    private ScoreCalculator _calculator;

    [SetUp]
    public void Setup()
    {
        _calculator = new ScoreCalculator();
    }

    [Test]
    [TestCase(new[] { 1, 1, 1, 4, 5 }, 3)]
    [TestCase(new[] { 1, 2, 3, 4, 5 }, 1)]
    [TestCase(new[] { 2, 2, 2, 2, 2 }, 0)]
    public void Ones_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.Ones), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 6, 6, 2, 3, 4 }, 12)]
    [TestCase(new[] { 1, 2, 3, 4, 5 }, 0)]
    public void Sixes_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.Sixes), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 3, 3, 3, 4, 5 }, 18)] // Sum of all: 3+3+3+4+5 = 18
    [TestCase(new[] { 3, 3, 4, 4, 5 }, 0)]  // No three of a kind
    public void ThreeOfAKind_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.ThreeOfAKind), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 4, 4, 4, 4, 2 }, 18)] // Sum of all: 4+4+4+4+2 = 18
    [TestCase(new[] { 4, 4, 4, 2, 2 }, 0)]  // No four of a kind
    public void FourOfAKind_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.FourOfAKind), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 2, 2, 3, 3, 3 }, 25)]
    [TestCase(new[] { 2, 2, 2, 2, 2 }, 0)]  // Sovereign is not a Full House by default rules
    [TestCase(new[] { 1, 2, 3, 4, 5 }, 0)]
    public void FullHouse_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.FullHouse), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 1, 2, 3, 4, 6 }, 30)]
    [TestCase(new[] { 2, 3, 4, 5, 1 }, 30)]
    [TestCase(new[] { 1, 1, 2, 3, 4 }, 30)] // Contains sequence with duplicates
    [TestCase(new[] { 1, 2, 4, 5, 6 }, 0)]
    public void SmallStraight_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.SmallStraight), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 1, 2, 3, 4, 5 }, 40)]
    [TestCase(new[] { 2, 3, 4, 5, 6 }, 40)]
    [TestCase(new[] { 1, 2, 3, 4, 6 }, 0)]
    public void LargeStraight_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.LargeStraight), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(new[] { 5, 5, 5, 5, 5 }, 50)]
    [TestCase(new[] { 1, 1, 1, 1, 2 }, 0)]
    public void Sovereign_CalculatesCorrectly(int[] dice, int expected)
    {
        Assert.That(_calculator.Calculate(dice, ScoreCategory.Yahtzee), Is.EqualTo(expected));
    }

    [Test]
    public void Chance_ReturnsSumOfAllDice()
    {
        int[] dice = { 1, 2, 3, 4, 5 }; // Sum = 15
        Assert.That(_calculator.Calculate(dice, ScoreCategory.Chance), Is.EqualTo(15));
    }
}