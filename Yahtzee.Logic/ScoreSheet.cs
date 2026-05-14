namespace Yahtzee.Logic;

public class ScoreSheet
{
    public Dictionary<ScoreCategory, int?> Scores { get; } = new();

    public ScoreSheet()
    {
        Scores.Add(ScoreCategory.Ones, null);
        Scores.Add(ScoreCategory.Twos, null);
        Scores.Add(ScoreCategory.Threes, null);
        Scores.Add(ScoreCategory.Fours, null);
        Scores.Add(ScoreCategory.Fives, null);
        Scores.Add(ScoreCategory.Sixes, null);

        Scores.Add(ScoreCategory.ThreeOfAKind, null);
        Scores.Add(ScoreCategory.FourOfAKind, null);
        Scores.Add(ScoreCategory.FullHouse, null);
        Scores.Add(ScoreCategory.SmallStraight, null);
        Scores.Add(ScoreCategory.LargeStraight, null);
        Scores.Add(ScoreCategory.Yahtzee, null);
        Scores.Add(ScoreCategory.Chance, null);
    }

    public bool IsAvailable(ScoreCategory category)
    {
        return Scores.ContainsKey(category) && Scores[category] == null;
    }

    public void Record(ScoreCategory category, int score)
    {
        if (!IsAvailable(category))
        {
            throw new InvalidOperationException($"Field {category} is already full.");
        }

        Scores[category] = score;
    }

    public int UpperSectionTotal
    {
        get
        {
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                ScoreCategory category = (ScoreCategory)i;

                sum += Scores[category] ?? 0;
            }

            return sum;
        }
    }

    public bool UpperBonusEarned => UpperSectionTotal >= 63;

    public int GrandTotal
    {
        get
        {
            int totalSum = UpperSectionTotal;
            for (int i = 6; i < 13; i++)
            {
                ScoreCategory category = (ScoreCategory)i;
                totalSum += Scores[category] ?? 0;
            }

            if (UpperBonusEarned) totalSum += 35;

            return totalSum;
        }
    }

    public bool IsComplete => Scores.Values.All(v => v != null);

    public IEnumerable<ScoreCategory> AvailableCategories => 
        Scores.Where(x => x.Value == null).Select(x => x.Key);
}