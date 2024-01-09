using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell TopCell, RightCell, BottomCell, LeftCell;

    [SerializeField] Transform _spawnPawnTransform;

    Pawn _currentPawn;

    //Cache 
    List<Cell> _adjacentSamePawn = new List<Cell>();

    public Pawn CurrentPawn { get => _currentPawn; }
    public List<Cell> AdjacentSamePawn { get => _adjacentSamePawn; }

    public void SetCurrentPawn(Pawn newPawn)
    {
        _currentPawn = newPawn;

        _currentPawn.transform.SetParent(_spawnPawnTransform);
        _currentPawn.transform.localPosition = Vector3.zero;

        CheckAdjacent();
    }

    public void ResetCell()
    {
        if (_currentPawn != null)
        {
            _currentPawn = null;
        }
    }

    public void CheckAdjacent()
    {
        int sameAdjacent = 0;

        if (TopCell != null)
        {
            if (TopCell.CurrentPawn != null)
            {
                if (TopCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
                {
                    sameAdjacent++;

                    _adjacentSamePawn.Add(TopCell);
                }
            }
        }

        if (RightCell != null)
        {
            if (RightCell.CurrentPawn != null)
            {
                if (RightCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
                {
                    sameAdjacent++;

                    _adjacentSamePawn.Add(RightCell);
                }
            }
        }

        if (BottomCell != null)
        {
            if (BottomCell.CurrentPawn != null)
            {
                if (BottomCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
                {
                    sameAdjacent++;

                    _adjacentSamePawn.Add(BottomCell);
                }
            }
        }

        if (LeftCell != null)
        {
            if (LeftCell.CurrentPawn != null)
            {
                if (LeftCell.CurrentPawn.PawnObject.type == CurrentPawn.PawnObject.type)
                {
                    sameAdjacent++;

                    _adjacentSamePawn.Add(LeftCell);
                }
            }
        }

        if (sameAdjacent >= 2)
        {
            Merge(this);
        }
    }

    public void Merge(Cell mergeCell)
    {
        foreach (Cell adjacentPawn in _adjacentSamePawn)
        {
            adjacentPawn.Merge(this);
        }

        _adjacentSamePawn.Clear();

        _currentPawn.transform.DOMove(mergeCell.CurrentPawn.transform.position, .2f).OnComplete(() =>
        {
            _currentPawn.transform.parent = null;
            _currentPawn.gameObject.SetActive(false);

            Pawn newPawn = null;

            if (mergeCell == this)
            {
                switch (_currentPawn.PawnObject.type)
                {
                    case PawnType.Cheese:
                        newPawn = PoolManager.Instance.SpawnFromPool("Mouse", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Mouse:
                        newPawn = PoolManager.Instance.SpawnFromPool("Rat", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Rat:
                        newPawn = PoolManager.Instance.SpawnFromPool("Cheese", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Cat:
                        newPawn = PoolManager.Instance.SpawnFromPool("Cheese", transform.position, transform.rotation).GetComponent<Pawn>();
                        break;
                    case PawnType.Dog:
                        newPawn = PoolManager.Instance.SpawnFromPool("Cheese", transform.position, transform.rotation).GetComponent<Pawn>();
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
