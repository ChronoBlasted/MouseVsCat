using System;
using UnityEngine;

public enum PawnType
{
    None = 0,
    Cherry = 1,
    Strawberry = 2,
    Grapes = 3,
    Banana = 4,
    Orange = 5,
    Apple = 6,
    Pear = 7,
    Ananas = 8,
    Watermelon = 9,
}

[CreateAssetMenu(fileName = "NewPawnObject", menuName = "GameScriptable/NewPawnObject", order = 1)]
public class PawnObject : ScriptableObject
{
    public PawnType type;
    public Sprite sprite;
    public int ScoreValue;
}

[Serializable]
public class PawnProb
{
    public PawnType type;
    public float prob;
}
