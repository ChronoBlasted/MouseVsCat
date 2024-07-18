using System;
using UnityEngine;

public enum PawnTier
{
    None = 0,
    Tier1 = 1,
    Tier2 = 2,
    Tier3 = 3,
    Tier4 = 4,
    Tier5 = 5,
    Tier6 = 6,
    Tier7 = 7,
    Tier8 = 8,
    Tier9 = 9,
    Joker1up = 100,
    JokerChoose = 101,
    SwitchCell = 102,
    DestroyCell = 103,
}

public enum PawnType
{
    CLASSIC,
    SPECIAL
}

[CreateAssetMenu(fileName = "NewPawnObject", menuName = "PawnObject/New pawn object", order = 1)]
public class PawnObject : ScriptableObject
{
    public PawnTier tier;
    public PawnType type;
    public int ScoreValue;
    public int Cost;
}

[Serializable]
public class PawnProb
{
    public PawnTier type;
    public float prob;
}
