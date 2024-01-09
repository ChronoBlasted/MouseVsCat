using UnityEngine;

public enum PawnType
{
    None = 0,
    Cheese = 1,
    Mouse = 2,
    Rat = 3,
    Cat = 4,
    Dog = 5,
}

[CreateAssetMenu(fileName = "NewPawnObject", menuName = "GameScriptable/NewPawnObject", order = 1)]
public class PawnObject : ScriptableObject
{
    public PawnType type;
    public Pawn pawnPrefab;
}
