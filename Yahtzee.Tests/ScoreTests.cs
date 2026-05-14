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
    public void Ones_WithThreeOnes_ReturnsThree()
    {
        // Arrange
        int[] dice = { 1, 1, 1, 4, 5 };

        // Act
        int result = _calculator.Calculate(dice, ScoreCategory.Ones);

        // Assert
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void FullHouse_WithThreeFoursAndTwoTwos_ReturnsTwentyFive()
    {
        // Arrange
        int[] dice = { 4, 4, 4, 2, 2 };

        // Act
        int result = _calculator.Calculate(dice, ScoreCategory.FullHouse);

        // Assert
        Assert.That(result, Is.EqualTo(25));
    }

    [Test]
    public void SmallStraight_WithSequenceOfFour_ReturnsThirty()
    {
        // Arrange
        int[] dice = { 1, 2, 3, 4, 6 };

        // Act
        int result = _calculator.Calculate(dice, ScoreCategory.SmallStraight);

        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    [Test]
    public void Yahtzee_WithFiveSameDice_ReturnsFifty()
    {
        // Arrange
        int[] dice = { 6, 6, 6, 6, 6 };

        // Act
        int result = _calculator.Calculate(dice, ScoreCategory.Yahtzee);

        // Assert
        Assert.That(result, Is.EqualTo(50));
    }
}