namespace Yahtzee.Logic;

public enum ScoreCategory           
{
    // Upper section (Table 1)
    Ones,
    Twos,
    Threes,
    Fours,
    Fives,
    Sixes,

    // Lower section (Table 2)
    ThreeOfAKind,
    FourOfAKind,
    FullHouse,
    SmallStraight,
    LargeStraight,
    Yahtzee,
    Chance
}
