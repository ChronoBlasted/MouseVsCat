using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer shadowRenderer;


    #region DragAndDrop
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] PawnObject _pawnObject;

    private float rotationSpeed = 0.05f;
    private float resetSpeed = 500f;
    private float maxRotationAngle = 45.0f;
    private float noMoveThreshold = 0.01f;

    Vector3 _mousePositionOffset;
    Vector3 lastMousePosition;


    Tweener movementTween;

    public BoxCollider2D BoxCollider { get => _boxCollider; }
    public PawnObject PawnObject { get => _pawnObject; set => _pawnObject = value; }

    #endregion

    public void Init()
    {
        spriteRenderer.sprite = _pawnObject.sprite;
        shadowRenderer.sprite = _pawnObject.sprite;
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

        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mouseVelocityX = (currentMousePosition.x - lastMousePosition.x) / Time.deltaTime;

        if (Mathf.Abs(mouseVelocityX) < noMoveThreshold)
        {
            float currentRotationAngle = transform.eulerAngles.z;
            if (currentRotationAngle > 180) currentRotationAngle -= 360; // Convert to -180 to 180 range
            float newRotationAngle = Mathf.MoveTowards(currentRotationAngle, 0, resetSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);
        }
        else
        {
            float rotationAmount = mouseVelocityX * rotationSpeed;

            float currentRotationAngle = transform.eulerAngles.z;
            if (currentRotationAngle > 180) currentRotationAngle -= 360; // Convert to -180 to 180 range

            float newRotationAngle = Mathf.Clamp(currentRotationAngle + rotationAmount, -maxRotationAngle, maxRotationAngle);

            transform.rotation = Quaternion.Euler(0, 0, newRotationAngle);

            lastMousePosition = currentMousePosition;
        }
    }


    private void OnMouseUp()
    {
        DropFeedbacks();

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
        Vector3 randomPunchRoate = new Vector3(0, 0, Random.Range(22.5f, 45f));

        if (Random.Range(0, 2) == 0) randomPunchRoate *= -1;

        transform.DOPunchRotation(randomPunchRoate, .1f, 20);

        spriteRenderer.transform.DOLocalMove(new Vector3(0, .2f, 0), .2f).SetEase(Ease.OutBack);

        spriteRenderer.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .2f).SetEase(Ease.OutBack);
        shadowRenderer.transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBack);
    }

    public void DropFeedbacks()
    {
        transform.DORotate(Vector3.zero, .2f);

        spriteRenderer.transform.DOLocalMove(Vector3.zero, .2f);

        spriteRenderer.transform.DOScale(Vector3.one, .2f);
        shadowRenderer.transform.DOScale(new Vector3(.8f, .8f, .8f), .2f);
    }

}
