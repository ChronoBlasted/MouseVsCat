using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer shadowRenderer;

    GameManager gameManager;

    #region DragAndDrop
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] PawnObject _pawnObject;
    [SerializeField] AnimationCurve _scaleInitCurve;

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

    private void Start()
    {
        gameManager = GameManager.Instance;

        SkinManager.Instance.OnSkinChange += UpdateSkin;
    }

    public void Init(bool bigScale = false)
    {
        if (bigScale)
        {
            spriteRenderer.DOFade(1, 0f);
            shadowRenderer.DOFade(1, 0f);

            transform.localScale = Vector3.zero;
        }
        else
        {
            spriteRenderer.DOFade(.5f, 0f);
            shadowRenderer.DOFade(.5f, 0f);
        }

        var sprite = DataUtils.Instance.GetSpriteBySkinAndTier(SkinManager.Instance.currentSkin, _pawnObject.tier);

        spriteRenderer.sprite = sprite;
        shadowRenderer.sprite = sprite;

        if (bigScale) transform.DOScale(Vector3.one, .5f).SetEase(_scaleInitCurve);
    }

    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (gameManager.GameState != GameState.GAME) return;

        if (movementTween.IsActive()) movementTween.Kill();

        _ = PickFeedbacks();

        _mousePositionOffset = transform.localPosition - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (gameManager.GameState != GameState.GAME) return;

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
        if (gameManager.GameState != GameState.GAME) return;

        _ = DropFeedbacks();

        var rayOrigin = GetMouseWorldPosition() - Camera.main.transform.position;
        var rayDirection = Camera.main.transform.position;

        RaycastHit2D hitInfo;

        if (hitInfo = Physics2D.Raycast(rayOrigin, rayDirection))
        {
            if (hitInfo.transform.tag == "DropArea")
            {
                Cell cell = hitInfo.transform.GetComponent<Cell>();

                if (_pawnObject.isSpecial == false)
                {
                    if (cell.CurrentPawn != null) ResetPawn(); // Case deja prise
                    else
                    {
                        cell.SetCurrentPawn(this);

                        if (cell.GetComponent<CellBoard>()) // Si cell board
                        {
                            if (tag != "Paradise") BoardGameManager.Instance.NewRound();
                            else BoardGameManager.Instance.ParadiseCell.ResetCell();
                        }
                        else BoardGameManager.Instance.NewRound(); // Si cell paradise
                    }
                }
                else
                {
                    PawnObjectSpecial pawnSpecial = (PawnObjectSpecial)_pawnObject;

                    if (cell.CurrentPawn == null)
                    {
                        if (cell.GetComponent<CellParadise>())
                        {
                            cell.SetCurrentPawn(this);
                            BoardGameManager.Instance.NewRound();
                        }
                        else
                        {
                            if (tag != "Paradise") BoardGameManager.Instance.NewRound();
                            else BoardGameManager.Instance.ParadiseCell.ResetCell();

                            pawnSpecial.OnDropWithNoPawn(this, cell);
                        }
                    }
                    else
                    {
                        if (cell.GetComponent<CellBoard>())
                        {
                            if (pawnSpecial.OnDropWithPawn(this, cell))
                            {
                                if (tag != "Paradise") BoardGameManager.Instance.NewRound();
                                else BoardGameManager.Instance.ParadiseCell.ResetCell();

                                ResetPawn();
                                PoolManager.Instance.ResetFromPool("Pawn", gameObject);
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
                }
            }
            else ResetPawn();
        }
        else ResetPawn();
    }

    public void ResetPawn()
    {
        if (movementTween.IsActive()) movementTween.Kill();
        movementTween = transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.OutBack);
        _boxCollider.enabled = true;
    }

    public async Task PickFeedbacks()
    {
        Vector3 randomPunchRoate = new Vector3(0, 0, Random.Range(22.5f, 45f));

        if (Random.Range(0, 2) == 0) randomPunchRoate *= -1;

        transform.DOPunchRotation(randomPunchRoate, .1f, 20);

        spriteRenderer.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), .2f).SetEase(Ease.OutBack);
        shadowRenderer.transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBack);

        spriteRenderer.sortingOrder = 10;
        shadowRenderer.sortingOrder = 9;

        await spriteRenderer.transform.DOLocalMove(new Vector3(0, .2f, 0), .2f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
    }

    public async Task DropFeedbacks()
    {
        _boxCollider.enabled = false;

        transform.DORotate(Vector3.zero, .2f);

        spriteRenderer.transform.DOScale(Vector3.one, .2f);
        shadowRenderer.transform.DOScale(new Vector3(.8f, .8f, .8f), .2f);

        await spriteRenderer.transform.DOLocalMove(Vector3.zero, .2f).OnComplete(() =>
        {
            spriteRenderer.sortingOrder = 6;
            shadowRenderer.sortingOrder = 5;
        }).AsyncWaitForCompletion(); ;
    }


    public void UpdateSkin(SkinType newSkin)
    {
        Sprite newSprite = DataUtils.Instance.GetSpriteBySkinAndTier(newSkin, PawnObject.tier);

        spriteRenderer.sprite = newSprite;
        shadowRenderer.sprite = newSprite;
    }
}
