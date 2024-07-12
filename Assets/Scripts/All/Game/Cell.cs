using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell TopCell, RightCell, BottomCell, LeftCell;
    [SerializeField] Transform _spawnPawnTransform;
    [SerializeField] ParticleSystem _beforeMergePS, _afterMergePS;
    [SerializeField] bool isParadiseCell;

    Pawn _currentPawn;
    Tweener movementTween;

    //Cache 
    List<Cell> _adjacentSameCell = new List<Cell>();

    public Pawn CurrentPawn { get => _currentPawn; }
    public List<Cell> AdjacentSameCell { get => _adjacentSameCell; }

    public void SetCurrentPawn(Pawn newPawn)
    {
        _currentPawn = newPawn;

        if (_currentPawn.tag != "Paradise") ProfileManager.Instance.UpdateScore(_currentPawn.PawnObject.ScoreValue);

        _currentPawn.tag = "Paradise";

        _currentPawn.transform.SetParent(_spawnPawnTransform);

        if (movementTween.IsActive()) movementTween.Kill();
        movementTween = _currentPawn.transform.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.OutCubic);

        _currentPawn.BoxCollider.enabled = isParadiseCell;

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


    public void ResetCell()
    {
        if (_currentPawn != null)
        {
            _currentPawn = null;
        }
    }

    public void CheckAdjacentCell(Cell baseCell)
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

    public void Merge(Cell mergeCell)
    {
        foreach (Cell adjacentPawn in _adjacentSameCell)
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

            PoolManager.Instance.ResetFromPool("Pawn", _currentPawn.gameObject);

            _currentPawn.ResetPawn();

            Pawn newPawn = null;

            if (mergeCell == this)
            {
                _afterMergePS.Play();

                newPawn = PoolManager.Instance.SpawnFromPool("Pawn", transform.position, transform.rotation).GetComponent<Pawn>();

                PawnType nextPawnType = DataUtils.Instance.GetNextPawnTypeByPawnType(_currentPawn.PawnObject.type);

                newPawn.PawnObject = DataUtils.Instance.GetPawnObjectByType(nextPawnType);

                newPawn.Init();
            }

            ResetCell();

            if (mergeCell == this)
            {
                SetCurrentPawn(newPawn);
            }
        });
    }
}
