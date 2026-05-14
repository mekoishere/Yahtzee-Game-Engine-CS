using System.Linq;

namespace Yahtzee.Logic;

public class GameSession
{
    private int _currentPlayerIndex = 0;

    public IReadOnlyList<Player> Players { get; }
    public Player CurrentPlayer => Players[_currentPlayerIndex];
    public IReadOnlyList<Die> CurrentDice { get; }
    public int RollsRemaining { get; private set; }

    public GameSession(List<string> playerNames)
    {
        if (playerNames == null || playerNames.Count < 2 || playerNames.Count > 4)
        {
            throw new ArgumentException("Player count must be between 2 and 4.");
        }

        Players = playerNames.Select(name => new Player(name)).ToList();
        
        var dice = new List<Die>();
        for (int i = 0; i < 5; i++) dice.Add(new Die());
        CurrentDice = dice;

        RollsRemaining = 3;
    }

    public void RollDice(IEnumerable<int> indicesToKeep)
    {
        if (RollsRemaining <= 0) 
            throw new InvalidOperationException("No more rolls remaining in this turn.");

        var toKeep = indicesToKeep?.ToHashSet() ?? new HashSet<int>();

        for (int i = 0; i < CurrentDice.Count; i++)
        {
            CurrentDice[i].IsLocked = toKeep.Contains(i);
            CurrentDice[i].Roll();
        }

        RollsRemaining--;
    }

    public void SelectCategory(ScoreCategory category)
    {
        if (RollsRemaining == 3)
            throw new InvalidOperationException("You must roll the dice at least once before selecting a category.");

        var calculator = new ScoreCalculator();
        int[] diceValues = CurrentDice.Select(d => d.Value).ToArray();
        int score = calculator.Calculate(diceValues, category);
        
        CurrentPlayer.ScoreSheet.Record(category, score);

        // Reset for next turn
        _currentPlayerIndex = (_currentPlayerIndex + 1) % Players.Count;
        RollsRemaining = 3;

        foreach (var die in CurrentDice)
        {
            die.IsLocked = false;
        }
    }

    public bool IsGameOver => Players.All(p => p.ScoreSheet.IsComplete);

    public Player? Winner => IsGameOver 
        ? Players.OrderByDescending(p => p.ScoreSheet.GrandTotal).FirstOrDefault() 
        : null;
}