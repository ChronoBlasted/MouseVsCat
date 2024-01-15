using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] PawnObject _pawnObject;
    Vector3 _mousePositionOffset;

    public PawnObject PawnObject { get => _pawnObject; }
    public BoxCollider2D BoxCollider { get => _boxCollider; }

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
        if (transform.position.y > -1.75f && transform.position.y < 3.85f && transform.position.x > -2.75f && transform.position.x < 2.75f)
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
