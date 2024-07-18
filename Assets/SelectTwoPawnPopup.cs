using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTwoPawnPopup : Popup
{
    public Cell OtherCell;

    Cell originCell;
    Pawn currentPawn;

    public override void Init()
    {
        base.Init();
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
    }

    public override void ClosePopup()
    {
        base.ClosePopup();
    }

    public void UpdateData(Pawn owner, Cell cellToInteract)
    {
        currentPawn = owner;
        originCell = cellToInteract;
    }

    public void HandleOnConfirm()
    {
        var pawnATier = currentPawn.PawnObject.tier;
        var pawnBTier = OtherCell.CurrentPawn.PawnObject.tier;

        originCell.ResetCell();
        OtherCell.ResetCell();

        Pawn tempPawn;

        //Set Pawn A to B cell
        tempPawn = PoolManager.Instance.SpawnFromPool("Pawn", OtherCell.SpawnPawnTransform.position, OtherCell.SpawnPawnTransform.rotation).GetComponent<Pawn>();
        tempPawn.transform.SetParent(OtherCell.SpawnPawnTransform);
        tempPawn.PawnObject = DataUtils.Instance.GetPawnObjectByTier(pawnBTier);
        tempPawn.Init(true);

        //Set Pawn B to A cell
        tempPawn = PoolManager.Instance.SpawnFromPool("Pawn", originCell.SpawnPawnTransform.position, originCell.SpawnPawnTransform.rotation).GetComponent<Pawn>();
        tempPawn.transform.SetParent(originCell.SpawnPawnTransform);
        tempPawn.PawnObject = DataUtils.Instance.GetPawnObjectByTier(pawnATier);
        tempPawn.Init(true);
    }
}
