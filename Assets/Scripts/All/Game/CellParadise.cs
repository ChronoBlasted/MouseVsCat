using UnityEngine;

public class CellParadise : Cell
{
    [SerializeField] BoxCollider2D dropCollider;
    public override void SetCurrentPawn(Pawn newPawn, bool isDraggedPawn = true)
    {
        base.SetCurrentPawn(newPawn, isDraggedPawn);

        _currentPawn.BoxCollider.enabled = true;

        _currentPawn.tag = "Paradise";

        dropCollider.enabled = false;
    }


    public override void ResetCell()
    {
        base.ResetCell();

        dropCollider.enabled = true;
    }
}
