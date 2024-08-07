using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] List<Cell> allCell;

    float _distance;
    float _closestDistance;
    Cell _closestCell;

    public void ResetBoard()
    {

    }

    public Cell FindClosestCell(Vector3 position)
    {
        _closestDistance = 100;

        foreach (Cell cell in allCell)
        {
            _distance = Vector3.Distance(position, cell.transform.position);

            if (_distance < _closestDistance)
            {
                _closestDistance = _distance;
                _closestCell = cell;
            }
        }

        return _closestCell;
    }

    [ContextMenu("Find all closest cell")]
    public void FindCellCloseToClose()
    {
        for (int i = 0; i < allCell.Count; i++)
        {
            if (i > 4)
            {
                //Top
                allCell[i].TopCell = allCell[i - 5];
            }
            else
            {
                allCell[i].TopCell = null;
            }

            if (((i - 4) % 5) != 0)
            {
                //Right
                allCell[i].RightCell = allCell[i + 1];
            }
            else
            {
                allCell[i].RightCell = null;
            }

            if ((i % 5) != 0)
            {
                //Left
                allCell[i].LeftCell = allCell[i - 1];
            }
            else
            {
                allCell[i].LeftCell = null;
            }

            if (i < allCell.Count - 5)
            {
                //Bottom
                allCell[i].BottomCell = allCell[i + 5];
            }
            else
            {
                allCell[i].BottomCell = null;
            }
        }
    }
}
