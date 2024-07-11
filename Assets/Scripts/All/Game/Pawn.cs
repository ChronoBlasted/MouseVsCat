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
        _mousePositionOffset = transform.localPosition - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.localPosition = GetMouseWorldPosition() + _mousePositionOffset;
    }


    private void OnMouseUp()
    {
        _boxCollider.enabled = false;

        var rayOrigin = GetMouseWorldPosition() - Camera.main.transform.position;
        var rayDirection = Camera.main.transform.position;

        RaycastHit2D hitInfo;

        if (hitInfo = Physics2D.Raycast(rayOrigin, rayDirection))
        {
            if (hitInfo.transform.tag == "DropArea")
            {
                Cell cell = hitInfo.transform.GetComponent<Cell>();

                if (cell.CurrentPawn != null)
                {
                    ResetPawn();
                }
                else
                {
                    if (transform.tag == "Paradise")
                    {
                        BoardGameManager.Instance.ParadiseCell.ResetCell();
                    }
                    else
                    {
                        BoardGameManager.Instance.NewRound();
                    }

                    cell.SetCurrentPawn(this);
                }
            }
            else
            {
                ResetPawn();
            }
        }
        else
        {
            ResetPawn();
        }
    }

    public void ResetPawn()
    {
        transform.localPosition = Vector3.zero;
        _boxCollider.enabled = true;
    }
}
