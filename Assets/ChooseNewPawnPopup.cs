using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNewPawnPopup : Popup
{
    public Pawn SelectedPawn;

    [SerializeField] Transform _pawnTransform;
    [SerializeField]
    List<PawnTier> _defaultCarousel = new List<PawnTier>
       { PawnTier.Tier1 ,
         PawnTier.Tier2 ,
         PawnTier.Tier3 ,
         PawnTier.Tier4 ,
         PawnTier.Tier5 };

    Cell originCell;
    Pawn currentPawn;

    public override void Init()
    {
        base.Init();

        InitCarousel(_defaultCarousel);
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
        originCell.SetCurrentPawn(SelectedPawn, false);
    }

    void InitCarousel(List<PawnTier> tierToSpawn)
    {
        foreach (Transform t in _pawnTransform)
        {
            Destroy(t.gameObject);
        }

        Pawn tempPawn;

        foreach (PawnTier tier in tierToSpawn)
        {
            tempPawn = PoolManager.Instance.SpawnFromPool("Pawn", _pawnTransform.position, _pawnTransform.rotation).GetComponent<Pawn>();
            tempPawn.transform.SetParent(_pawnTransform);
            tempPawn.PawnObject = DataUtils.Instance.GetPawnObjectByTier(tier);
            tempPawn.Init();
        }
    }
}
