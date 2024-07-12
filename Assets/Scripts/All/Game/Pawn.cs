using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    #region DragAndDrop
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] PawnObject _pawnObject;
    Vector3 _mousePositionOffset;

    Tweener movementTween;

    public PawnObject PawnObject { get => _pawnObject; }
    public BoxCollider2D BoxCollider { get => _boxCollider; }

    #endregion

    public void Init()
    {
        switch (_pawnObject.type)
        {

        }
    }

    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (movementTween.IsActive()) movementTween.Kill();

        PickFeedbacks();

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
        if (movementTween.IsActive()) movementTween.Kill();
        movementTween = transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.OutBack);
        _boxCollider.enabled = true;
    }

    public void PickFeedbacks()
    {
        transform.DOPunchRotation(Vector3.one, .2f);

        spriteRenderer.transform.DOLocalMove(new Vector3(0, 1, 0), .2f);

        spriteRenderer.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .2f);
    }

    public void DropFeedbacks()
    {
        spriteRenderer.transform.DOLocalMove(new Vector3(0, 1, 0), .2f);

        spriteRenderer.transform.DOScale(Vector3.one, .2f);

    }
}
