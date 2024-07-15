using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBoard : Cell
{
    public CellBoard TopCell, RightCell, BottomCell, LeftCell;

    //Cache 
    List<CellBoard> _adjacentSameCell = new List<CellBoard>();

    public List<CellBoard> AdjacentSameCell { get => _adjacentSameCell; }

    public override void SetCurrentPawn(Pawn newPawn, bool isDraggedPawn = true)
    {
        base.SetCurrentPawn(newPawn, isDraggedPawn);

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
                    if (TopCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
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
                    if (RightCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
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
                    if (BottomCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
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
                    if (LeftCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
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

        if (mergeCell == this)
        {
            _beforeMergePS.Play();
        }

        _currentPawn.transform.DOMove(mergeCell._spawnPawnTransform.transform.position, .2f).OnComplete(() =>
        {
            _currentPawn.transform.parent = null;
            _currentPawn.ResetPawn();
            PoolManager.Instance.ResetFromPool("Pawn", _currentPawn.gameObject);

            Pawn newPawn = null;

            if (mergeCell == this)
            {
                _afterMergePS.Play();

                newPawn = PoolManager.Instance.SpawnFromPool("Pawn", transform.position, transform.rotation).GetComponent<Pawn>();

                PawnTier nextPawnType = DataUtils.Instance.GetNextPawnTypeByPawnType(_currentPawn.PawnObject.type);

                newPawn.PawnObject = DataUtils.Instance.GetPawnObjectByType(nextPawnType);

                newPawn.Init(true);
            }

            ResetCell();

            if (mergeCell == this)
            {
                SetCurrentPawn(newPawn);
            }
        });
    }
}
