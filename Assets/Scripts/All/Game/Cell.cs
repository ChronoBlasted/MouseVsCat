using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell TopCell, RightCell, BottomCell, LeftCell;
    [SerializeField] Transform _spawnPawnTransform;
    [SerializeField] ParticleSystem _beforeMergePS, _afterMergePS;

    Pawn _currentPawn;

    //Cache 
    List<Cell> _adjacentSameCell = new List<Cell>();

    public Pawn CurrentPawn { get => _currentPawn; }
    public List<Cell> AdjacentSameCell { get => _adjacentSameCell; }

    public void SetCurrentPawn(Pawn newPawn)
    {
        _currentPawn = newPawn;

        ProfileManager.Instance.UpdateScore(_currentPawn.PawnObject.ScoreValue);

        _currentPawn.transform.SetParent(_spawnPawnTransform);
        _currentPawn.transform.localPosition = Vector3.zero;

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

        _currentPawn.transform.DOMove(mergeCell.CurrentPawn.transform.position, .2f).OnComplete(() =>
        {
            _currentPawn.transform.parent = null;
            _currentPawn.gameObject.SetActive(false);

            _currentPawn.BoxCollider.enabled = true;

            Pawn newPawn = null;

            if (mergeCell == this)
            {
                _afterMergePS.Play();

                switch (_currentPawn.PawnObject.type)
                {
                    case PawnType.Cherry:
                        newPawn = PoolManager.Instance.SpawnFromPool("Strawberry", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Strawberry:
                        newPawn = PoolManager.Instance.SpawnFromPool("Grapes", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Grapes:
                        newPawn = PoolManager.Instance.SpawnFromPool("Banana", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Banana:
                        newPawn = PoolManager.Instance.SpawnFromPool("Orange", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Orange:
                        newPawn = PoolManager.Instance.SpawnFromPool("Apple", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Apple:
                        newPawn = PoolManager.Instance.SpawnFromPool("Pear", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Pear:
                        newPawn = PoolManager.Instance.SpawnFromPool("Ananas", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Ananas:
                        newPawn = PoolManager.Instance.SpawnFromPool("Watermelon", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Watermelon:
                        newPawn = null;
                        break;
                    default:
                        break;
                }
            }

            ResetCell();

            if (mergeCell == this)
            {
                SetCurrentPawn(newPawn);
            }
        });
    }
}
