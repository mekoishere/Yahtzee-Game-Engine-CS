namespace Yahtzee.Logic;

public class Player
{
    private string name;
    ScoreSheet scoreSheet;
    
    public Player(string name)
    {
        this.name = name;
        scoreSheet = new ScoreSheet();
    }
}