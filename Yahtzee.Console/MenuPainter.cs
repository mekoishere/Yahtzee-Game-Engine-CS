using Yahtzee.Logic;

namespace Yahtzee.Console;

public static class MenuPainter
{
    public static void ShowDice(IReadOnlyList<Die> dice)
    {
        System.Console.WriteLine("\n--- YOUR DICE ---");

        string[][] diceFaces = dice.Select(d => GetDiceAscii(d.Value)).ToArray();
        int rows = diceFaces[0].Length;

        for (int i = 0; i < dice.Count; i++)
        {
            System.Console.Write($"    [{i}]     ");
        }
        System.Console.WriteLine();

        for (int r = 0; r < rows; r++)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i].IsLocked)
                {
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                }

                System.Console.Write(diceFaces[i][r] + "  ");
                System.Console.ResetColor();
            }
            System.Console.WriteLine();
        }
        
        System.Console.WriteLine("-------------------");
    }

    private static string[] GetDiceAscii(int value)
    {
        return value switch
        {
            1 => ["+---------+", "|         |", "|    O    |", "|         |", "+---------+"],
            2 => ["+---------+", "|  O      |", "|         |", "|      O  |", "+---------+"],
            3 => ["+---------+", "|  O      |", "|    O    |", "|      O  |", "+---------+"],
            4 => ["+---------+", "|  O   O  |", "|         |", "|  O   O  |", "+---------+"],
            5 => ["+---------+", "|  O   O  |", "|    O    |", "|  O   O  |", "+---------+"],
            6 => ["+---------+", "|  O   O  |", "|  O   O  |", "|  O   O  |", "+---------+"],
            _ => ["", "", "", "", ""]
        };
    }

    public static void ShowScoreSheet(Player player)
    {
        System.Console.WriteLine($"\n--- SCORE SHEET: {player.Name} ---");
        System.Console.WriteLine("-------------------------------");
        
        foreach (var entry in player.ScoreSheet.Scores)
        {
            string categoryName = entry.Key.ToString();
            string scoreValue = entry.Value?.ToString() ?? "-";
            
            System.Console.WriteLine($"{categoryName,-20} | {scoreValue,5}");

            if (entry.Key == ScoreCategory.Sixes)
            {
                System.Console.WriteLine("-------------------------------");
                System.Console.WriteLine($"{"Upper Section Total",-20} | {player.ScoreSheet.UpperSectionTotal,5}");
                System.Console.WriteLine($"{"Upper Bonus (+35)",-20} | {(player.ScoreSheet.UpperBonusEarned ? "35" : "0"),5}");
                System.Console.WriteLine("-------------------------------");
            }
        }

        System.Console.WriteLine("-------------------------------");
        System.Console.WriteLine($"{"GRAND TOTAL",-20} | {player.ScoreSheet.GrandTotal,5}");
        System.Console.WriteLine("-------------------------------");
    }

    public static void ShowAllScoreSheets(IReadOnlyList<Player> players)
    {
        System.Console.WriteLine("\n--- CURRENT RANKING ---");
        foreach (var p in players)
        {
            System.Console.WriteLine($"{p.Name,-15} : {p.ScoreSheet.GrandTotal} pts");
        }
    }

    public static void ShowAvailableCategories(Player player, IReadOnlyList<Die> dice, ScoreCalculator calc)
    {
        System.Console.WriteLine("\n--- AVAILABLE CATEGORIES ---");
        var available = player.ScoreSheet.AvailableCategories.ToList();
        int[] diceValues = dice.Select(d => d.Value).ToArray();

        for (int i = 0; i < available.Count; i++)
        {
            var cat = available[i];
            int potentialScore = calc.Calculate(diceValues, cat);
            System.Console.WriteLine($"[{i}] {cat,-15} : {potentialScore} pts");
        }
    }

    public static void ShowGameOver(IReadOnlyList<Player> players)
    {
        System.Console.Clear();
        System.Console.WriteLine("***************************");
        System.Console.WriteLine("*       GAME OVER!        *");
        System.Console.WriteLine("***************************\n");
        
        var ranking = players.OrderByDescending(p => p.ScoreSheet.GrandTotal).ToList();
        for (int i = 0; i < ranking.Count; i++)
        {
            System.Console.WriteLine($"{i + 1}. {ranking[i].Name}: {ranking[i].ScoreSheet.GrandTotal} pts");
        }

        System.Console.WriteLine($"\nWINNER: {ranking[0].Name}!");
    }
}