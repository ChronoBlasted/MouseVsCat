using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBoard : Cell
{
    public CellBoard TopCell, RightCell, BottomCell, LeftCell;
    [SerializeField] ParticleSystem _beforeMergePS, _afterMergePS;
    [SerializeField] FloatingText _floatingText;


    //Cache 
    List<CellBoard> _adjacentSameCell = new List<CellBoard>();

    public List<CellBoard> AdjacentSameCell { get => _adjacentSameCell; }

    public override void SetCurrentPawn(Pawn newPawn, bool isDraggedPawn = true)
    {
        base.SetCurrentPawn(newPawn, isDraggedPawn);

        ProfileManager.Instance.UpdateScore(_currentPawn.PawnObject.ScoreValue);

        _currentPawn.BoxCollider.enabled = false;

        CheckAdjacentCell(null);

        if (_adjacentSameCell.Count >= 2) Merge(this);
        else if (_adjacentSameCell.Count >= 1)
        {
            foreach (var cell in _adjacentSameCell)
            {
                cell.CheckAdjacentCell(this);

                if (cell.AdjacentSameCell.Count >= 1)
                {
                    Merge(this);
                }
            }
        }
    }


    public override void ResetCell()
    {
        base.ResetCell();
    }

    public void CheckAdjacentCell(CellBoard baseCell)
    {
        _adjacentSameCell.Clear();

        if (TopCell != null)
        {
            if (TopCell != baseCell)
            {
                if (TopCell.CurrentPawn != null)
                {
                    if (TopCell.CurrentPawn.PawnObject.tier == CurrentPawn.PawnObject.tier)
                    {
                        _adjacentSameCell.Add(TopCell);
                    }
                }
            }
        }

        if (RightCell != null)
        {
            if (RightCell != baseCell)
            {
                if (RightCell.CurrentPawn != null)
                {
                    if (RightCell.CurrentPawn.PawnObject.tier == CurrentPawn.PawnObject.tier)
                    {
                        _adjacentSameCell.Add(RightCell);
                    }
                }
            }
        }

        if (BottomCell != null)
        {
            if (BottomCell != baseCell)
            {
                if (BottomCell.CurrentPawn != null)
                {
                    if (BottomCell.CurrentPawn.PawnObject.tier == CurrentPawn.PawnObject.tier)
                    {
                        _adjacentSameCell.Add(BottomCell);
                    }
                }
            }
        }

        if (LeftCell != null)
        {
            if (LeftCell != baseCell)
            {
                if (LeftCell.CurrentPawn != null)
                {
                    if (LeftCell.CurrentPawn.PawnObject.tier == CurrentPawn.PawnObject.tier)
                    {
                        _adjacentSameCell.Add(LeftCell);
                    }
                }
            }
        }
    }

    public void Merge(CellBoard mergeCell)
    {
        foreach (CellBoard adjacentPawn in _adjacentSameCell)
        {
            adjacentPawn.CheckAdjacentCell(this);

            adjacentPawn.Merge(mergeCell);
        }

        BoardGameManager.Instance.mergeStreak++;

        ProfileManager.Instance.AddCoins(BoardGameManager.Instance.mergeStreak);

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        var floatText = Instantiate(_floatingText, screenPosition, Quaternion.identity, UIManager.Instance.MainCanvas.transform);

        floatText.Init(BoardGameManager.Instance.mergeStreak + "<sprite=0>", Color.white);
        // Do Floating text

        if (mergeCell == this)
        {
            _beforeMergePS.Play();

            BoardGameManager.Instance.mergeStreak = 0;
        }

        var currentPawn = _currentPawn;

        ResetCell();

        currentPawn.transform.DOScale(Vector3.zero, .1f).SetDelay(.1f).SetEase(Ease.InBack);
        currentPawn.transform.DOMove(mergeCell._spawnPawnTransform.transform.position, .2f).OnComplete(() =>
        {
            currentPawn.transform.parent = null;

            currentPawn.ResetPawn();

            PoolManager.Instance.ResetFromPool("Pawn", currentPawn.gameObject);

            Pawn newPawn = null;

            if (mergeCell == this)
            {
                _afterMergePS.Play();

                newPawn = PoolManager.Instance.SpawnFromPool("Pawn", transform.position, transform.rotation).GetComponent<Pawn>();

                PawnTier nextPawnType = DataUtils.Instance.GetNextPawnTierByPawnTier(currentPawn.PawnObject.tier);

                newPawn.PawnObject = DataUtils.Instance.GetPawnObjectByTier(nextPawnType);

                newPawn.Init(true);
            }

            if (mergeCell == this)
            {
                SetCurrentPawn(newPawn);
            }
        });
    }
}
