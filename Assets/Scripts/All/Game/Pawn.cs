using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] PawnObject _pawnObject;
    Vector3 _mousePositionOffset;

    public PawnObject PawnObject { get => _pawnObject; }

    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        _mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + _mousePositionOffset;
    }


    private void OnMouseUp()
    {
        if (transform.position.y > -3.3f && transform.position.y < 4.4f && transform.position.x > -3.3f && transform.position.x < 3.3f)
        {
            var cell = BoardGameManager.Instance.Board.FindClosestCell(transform.position);

            if (cell.CurrentPawn != null)
            {
                transform.position = BoardGameManager.Instance.NewPawnSpawn.position;
            }
            else
            {
                cell.SetCurrentPawn(this);
                BoardGameManager.Instance.NewRound();
            }
        }
        else
        {
            transform.position = BoardGameManager.Instance.NewPawnSpawn.position;
        }


    }


}
