using NUnit.Framework;
using Yahtzee.Logic;
using System.Linq;
using System;

namespace Yahtzee.Tests;

public class ScoreSheetTests
{
    private ScoreSheet _scoreSheet;

    [SetUp]
    public void Setup()
    {
        _scoreSheet = new ScoreSheet();
    }

    [Test]
    public void InitialState_IsCorrect()
    {
        Assert.That(_scoreSheet.UpperSectionTotal, Is.EqualTo(0));
        Assert.That(_scoreSheet.UpperBonusEarned, Is.False);
        Assert.That(_scoreSheet.GrandTotal, Is.EqualTo(0));
        Assert.That(_scoreSheet.IsComplete, Is.False);
        Assert.That(_scoreSheet.AvailableCategories.Count(), Is.EqualTo(13));
    }

    [Test]
    public void Record_UpdatesScore()
    {
        _scoreSheet.Record(ScoreCategory.Ones, 5);
        Assert.That(_scoreSheet.Scores[ScoreCategory.Ones], Is.EqualTo(5));
        Assert.That(_scoreSheet.UpperSectionTotal, Is.EqualTo(5));
    }

    [Test]
    public void Record_OnOccupiedSlot_ThrowsException()
    {
        _scoreSheet.Record(ScoreCategory.Ones, 5);
        Assert.Throws<InvalidOperationException>(() => _scoreSheet.Record(ScoreCategory.Ones, 3));
    }

    [Test]
    public void UpperBonus_Earned_WhenTotalIs63()
    {
        _scoreSheet.Record(ScoreCategory.Ones, 3);   // 3
        _scoreSheet.Record(ScoreCategory.Twos, 6);   // 6
        _scoreSheet.Record(ScoreCategory.Threes, 9);  // 9
        _scoreSheet.Record(ScoreCategory.Fours, 12);  // 12
        _scoreSheet.Record(ScoreCategory.Fives, 15);  // 15
        _scoreSheet.Record(ScoreCategory.Sixes, 18);  // 18
        // Total: 3+6+9+12+15+18 = 63

        Assert.That(_scoreSheet.UpperSectionTotal, Is.EqualTo(63));
        Assert.That(_scoreSheet.UpperBonusEarned, Is.True);
        Assert.That(_scoreSheet.GrandTotal, Is.EqualTo(63 + 35));
    }

    [Test]
    public void UpperBonus_NotEarned_WhenTotalIs62()
    {
        _scoreSheet.Record(ScoreCategory.Ones, 2);   // 2
        _scoreSheet.Record(ScoreCategory.Twos, 6);   // 6
        _scoreSheet.Record(ScoreCategory.Threes, 9);  // 9
        _scoreSheet.Record(ScoreCategory.Fours, 12);  // 12
        _scoreSheet.Record(ScoreCategory.Fives, 15);  // 15
        _scoreSheet.Record(ScoreCategory.Sixes, 18);  // 18
        // Total: 2+6+9+12+15+18 = 62

        Assert.That(_scoreSheet.UpperSectionTotal, Is.EqualTo(62));
        Assert.That(_scoreSheet.UpperBonusEarned, Is.False);
        Assert.That(_scoreSheet.GrandTotal, Is.EqualTo(62));
    }

    [Test]
    public void GrandTotal_CalculatesCorrectly()
    {
        _scoreSheet.Record(ScoreCategory.Ones, 3);          // 3
        _scoreSheet.Record(ScoreCategory.Yahtzee, 50);      // 50
        _scoreSheet.Record(ScoreCategory.Chance, 15);       // 15
        
        Assert.That(_scoreSheet.GrandTotal, Is.EqualTo(3 + 50 + 15));
    }

    [Test]
    public void IsComplete_True_WhenAllSlotsFilled()
    {
        var categories = Enum.GetValues<ScoreCategory>();
        foreach (var cat in categories)
        {
            _scoreSheet.Record(cat, 0);
        }

        Assert.That(_scoreSheet.IsComplete, Is.True);
        Assert.That(_scoreSheet.AvailableCategories.Count(), Is.EqualTo(0));
    }
}