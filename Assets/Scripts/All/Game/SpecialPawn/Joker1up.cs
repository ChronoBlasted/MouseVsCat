using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJoker1upPawn", menuName = "PawnObject/New joker 1 up pawn", order = 2)]
public class Joker1upPawn : PawnObjectSpecial
{
    [SerializeField] int amountOfBonus = 1;
    public override bool OnDropWithPawn(Pawn owner, Cell cellToInteract)
    {
        var newPawn = PoolManager.Instance.SpawnFromPool("Pawn", cellToInteract.transform.position, cellToInteract.transform.rotation).GetComponent<Pawn>();
        PawnTier nextPawnType = DataUtils.Instance.GetNextPawnTierByPawnTier(cellToInteract.CurrentPawn.PawnObject.tier);
        newPawn.PawnObject = DataUtils.Instance.GetPawnObjectByTier(nextPawnType);
        newPawn.Init(true);

        cellToInteract.SetDefaultCell();
        cellToInteract.SetCurrentPawn(newPawn);

        return true;
    }

    public override bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
        owner.ResetPawn();
        PoolManager.Instance.ResetFromPool("Pawn", owner.gameObject);

        return true;
    }
}

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

[CreateAssetMenu(fileName = "NewJokerChoose", menuName = "PawnObject/New joker choose pawn", order = 3)]
public class JokerChoosePawn : PawnObjectSpecial
{
    public override bool OnDropWithPawn(Pawn owner, Cell cellToInteract)
    {
        return false;
    }

    public override bool OnDropWithNoPawn(Pawn owner, Cell cellToInteract)
    {
        UIManager.Instance.ChooseNewPawnPopup.UpdateData(owner, cellToInteract);
        UIManager.Instance.AddPopup(UIManager.Instance.ChooseNewPawnPopup);

        return true;
    }
}

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