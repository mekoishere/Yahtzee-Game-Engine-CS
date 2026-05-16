using System;
using System.Collections.Generic;
using System.Linq;
using Yahtzee.Logic;

namespace Yahtzee.Console;

class Program
{
    static void Main(string[] args)
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        System.Console.WriteLine("===========================");
        System.Console.WriteLine("        YAHTZEE GAME       ");
        System.Console.WriteLine("===========================\n");

        System.Console.WriteLine("How many players? (2-4):");
        int playerCount;
        while (true)
        {
            if (int.TryParse(System.Console.ReadLine(), out playerCount) && playerCount >= 2 && playerCount <= 4)
            {
                break;
            }
            System.Console.WriteLine("Invalid input. Please enter a number between 2 and 4.");
        }

        List<string> playerNames = new List<string>();
        for (int i = 0; i < playerCount; i++)
        {
            System.Console.WriteLine($"Player {i + 1} name:");
            string name = System.Console.ReadLine()?.Trim() ?? $"Player {i + 1}";
            playerNames.Add(string.IsNullOrWhiteSpace(name) ? $"Player {i + 1}" : name);
        }

        GameSession session = new GameSession(playerNames);

        while (!session.IsGameOver)
        {
            System.Console.Clear();
            Player currentPlayer = session.CurrentPlayer;
            System.Console.WriteLine($"\nIt's {currentPlayer.Name}'s turn!");

            MenuPainter.ShowScoreSheet(currentPlayer);

            session.RollDice(new int[0]);

            while (session.RollsRemaining > 0)
            {
                MenuPainter.ShowDice(session.CurrentDice);
                System.Console.WriteLine($"Rolls remaining: {session.RollsRemaining}");
                System.Console.WriteLine("Enter dice indices to KEEP (e.g., '0 2 4'), 'R' to re-roll all, or 'S' to score:");

                string input = System.Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (input == "S") break;

                if (input == "R")
                {
                    session.RollDice(null);
                    continue;
                }

                List<int> toKeep = new List<int>();
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int index) && index >= 0 && index <= 4)
                    {
                        toKeep.Add(index);
                    }
                }

                session.RollDice(toKeep);
            }

            if (session.RollsRemaining == 0)
            {
                MenuPainter.ShowDice(session.CurrentDice);
                System.Console.WriteLine("No rolls remaining.");
            }

            while (true)
            {
                var calc = new ScoreCalculator();
                MenuPainter.ShowAvailableCategories(currentPlayer, session.CurrentDice, calc);
                var availableCategories = currentPlayer.ScoreSheet.AvailableCategories.ToList();
                System.Console.Write("\nSelect a category by index: ");

                if (int.TryParse(System.Console.ReadLine(), out int catInput) && catInput >= 0 && catInput < availableCategories.Count)
                {
                    session.SelectCategory(availableCategories[catInput]);
                    break;
                }
                
                System.Console.WriteLine("Invalid index. Please try again.");
            }

            System.Console.WriteLine("\nPress Enter to continue to the next player...");
            System.Console.ReadLine();
        }

        MenuPainter.ShowGameOver(session.Players);
        System.Console.WriteLine("\nPress Enter to exit...");
        System.Console.ReadLine();
    }
}