namespace Yahtzee.Logic;

public class ScoreCalculator
{
    public int Calculate(int[] dice, ScoreCategory category)
    {
        return category switch
        {
            // Górna sekcja (Suma oczek o danej wartości)
            ScoreCategory.Ones   => SumSpecificValue(dice, 1),
            ScoreCategory.Twos   => SumSpecificValue(dice, 2),
            ScoreCategory.Threes => SumSpecificValue(dice, 3),
            ScoreCategory.Fours  => SumSpecificValue(dice, 4),
            ScoreCategory.Fives  => SumSpecificValue(dice, 5),
            ScoreCategory.Sixes  => SumSpecificValue(dice, 6),

            // Dolna sekcja
            ScoreCategory.ThreeOfAKind  => HasNOfAKind(dice, 3),
            ScoreCategory.FourOfAKind   => HasNOfAKind(dice, 4),
            ScoreCategory.FullHouse     => IsFullHouse(dice) ? 25 : 0,
            ScoreCategory.SmallStraight => IsSmallStraight(dice) ? 30 : 0,
            ScoreCategory.LargeStraight => IsLargeStraight(dice) ? 40 : 0,
            ScoreCategory.Yahtzee       => IsYahtzee(dice) ? 50 : 0,
            ScoreCategory.Chance        => SumAll(dice),
            
            _ => throw new ArgumentOutOfRangeException(nameof(category)) 
        };
    }

    private int HasNOfAKind(int[] dice, int n)
    {
        int[] diceValueQuantity = GetDiceValueQuantity(dice);

        for (int i = 1; i <= 6; i++)
        {
            if (diceValueQuantity[i] >= n)
            {
                return SumAll(dice);
            }
        }

        return 0;
    }

    private bool IsFullHouse(int[] dice)
    {
        int[] diceValueQuantity = GetDiceValueQuantity(dice);
        bool hasThree = false;
        bool hasTwo = false;

        for (int i = 1; i <= 6; i++)
        {
            if (diceValueQuantity[i] == 3) hasThree = true;
            if (diceValueQuantity[i] == 2) hasTwo = true;
        }
        
        return hasThree && hasTwo;
    }

    private bool IsYahtzee(int[] dice)
    {
        int[] diceValueQuantity = GetDiceValueQuantity(dice);
        for (int i = 1; i <= 6; i++)
        {
            if (diceValueQuantity[i] == 5) return true;
        }
        return false;
    }

    private bool IsSmallStraight(int[] dice)
    {
        int[] diceValueQuantity = GetDiceValueQuantity(dice);
        bool NInARow = false;
        int counter = 0;
        for (int i = 1; i <= 6; i++)
        {
            if(diceValueQuantity[i] >= 1) counter++;
            else counter = 0;
            
            if (counter >= 4) NInARow = true;
        }
            
        return NInARow;
    }

    private bool IsLargeStraight(int[] dice)
    {
        int[] diceValueQuantity = GetDiceValueQuantity(dice);
        bool NInARow = false;
        int counter = 0;
        for (int i = 1; i <= 6; i++)
        {
            if(diceValueQuantity[i] >= 1) counter++;
            else counter = 0;
            
            if (counter >= 5) NInARow = true;
        }
            
        return NInARow;
    }

    private int[] GetDiceValueQuantity(int[] dice)
    {
        int[] diceValueQuantity = new int[7];
        foreach (int die in dice)
        {
            diceValueQuantity[die]++;
        }
        return diceValueQuantity;
    }

    private int SumSpecificValue(int[] dice, int value)
    {
        int sum = 0;
        foreach (int die in dice)
        {
            if (die == value)
            {
                sum += die;
            }
        }
        return sum;
    }

    private int SumAll(int[] dice)
    {
        int sum = 0;
        foreach (int die in dice)
        {
            sum += die;
        }
        return sum;
    }
}