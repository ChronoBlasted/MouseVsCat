using UnityEngine;

[CreateAssetMenu(fileName = "NewSwitchCell", menuName = "PawnObject/New switch cell pawn", order = 5)]
public class SwitchCellPawn : PawnObjectSpecial
{
    public override bool OnDropWithPawn(Pawn owner, Cell cellToInteract)
    {
        UIManager.Instance.SelectTwoPawnPopup.UpdateData(owner, cellToInteract);
        UIManager.Instance.AddPopup(UIManager.Instance.SelectTwoPawnPopup);

        return false;
    }

    public override bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
        owner.ResetPawn();
        PoolManager.Instance.ResetFromPool("Pawn", owner.gameObject);

        return true;
    }
}