using System;
using UnityEngine;

public abstract class PawnObjectSpecial : PawnObject
{
    public virtual bool OnDropWithPawn(Pawn owner,Cell cellToInteract)
    {
        return false;
    }

    public virtual bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
        return false;
    }
}
