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
        _mousePositionOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + _mousePositionOffset;
    }


    private void OnMouseUp()
    {
        _boxCollider.enabled = false;

        var rayOrigin = GetMouseWorldPosition() - Camera.main.transform.position;
        var rayDirection = Camera.main.transform.position;

        RaycastHit2D hitInfo;

        Debug.DrawRay(rayOrigin, rayDirection, Color.green, 5f);

        if (hitInfo = Physics2D.Raycast(rayOrigin, rayDirection))
        {

            if (hitInfo.transform.tag == "DropArea")
            {
                Cell cell = hitInfo.transform.GetComponent<Cell>();

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
                _boxCollider.enabled = true;

            }
        }
        else
        {
            transform.position = BoardGameManager.Instance.NewPawnSpawn.position;
            _boxCollider.enabled = true;
        }
    }
}
