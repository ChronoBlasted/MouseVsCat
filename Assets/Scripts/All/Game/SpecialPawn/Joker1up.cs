using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJoker1up", menuName = "PawnObject/New joker 1 up", order = 2)]
public class Joker1up : PawnObjectSpecial
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