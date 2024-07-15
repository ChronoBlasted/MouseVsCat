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
}

[CreateAssetMenu(fileName = "NewPawnObject", menuName = "GameScriptable/NewPawnObject", order = 1)]
public class PawnObject : ScriptableObject
{
    public PawnTier type;
    public Sprite sprite;
    public int ScoreValue;
}

[Serializable]
public class PawnProb
{
    public PawnTier type;
    public float prob;
}
