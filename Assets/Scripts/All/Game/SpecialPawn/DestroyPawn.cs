using UnityEngine;

[CreateAssetMenu(fileName = "NewDestroyPawn", menuName = "PawnObject/New destroy pawn", order = 4)]
public class DestroyPawn : PawnObjectSpecial
{
    public override bool OnDropWithPawn(Pawn owner, Cell cellToInteract)
    {
        cellToInteract.SetDefaultCell();

        return true;
    }

    public override bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
        owner.ResetPawn();
        PoolManager.Instance.ResetFromPool("Pawn", owner.gameObject);

        return true;
    }
}