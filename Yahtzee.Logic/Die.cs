namespace Yahtzee.Logic;

public class Die
{
    private static readonly Random _random = new();
    public int Value { get; private set; }
    public bool IsLocked { get; set; }

    public void Roll()
    {
        if (!IsLocked)
        {
            Value = _random.Next(1, 7);
        }
    }
}