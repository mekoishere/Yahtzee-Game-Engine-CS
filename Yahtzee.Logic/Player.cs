namespace Yahtzee.Logic;

public class Player
{
    public string Name { get; }
    public ScoreSheet ScoreSheet { get; }

    public Player(string name)
    {
        Name = name;
        ScoreSheet = new ScoreSheet();
    }
}