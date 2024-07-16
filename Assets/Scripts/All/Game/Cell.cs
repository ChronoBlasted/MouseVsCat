using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] protected Transform _spawnPawnTransform;
    [SerializeField] SpriteRenderer _spriteRenderer;

    protected Tweener movementTween;
    protected Pawn _currentPawn;
    public Pawn CurrentPawn { get => _currentPawn; }
    public Transform SpawnPawnTransform { get => _spawnPawnTransform; }

    private void Start()
    {
        SkinManager.Instance.OnSkinChange += UpdateSkin;
    }


    public virtual void SetCurrentPawn(Pawn newPawn, bool isDraggedPawn = true)
    {
        _currentPawn = newPawn;

        if (isDraggedPawn)
        {
            _currentPawn.transform.SetParent(_spawnPawnTransform);

            if (movementTween.IsActive()) movementTween.Kill();
            movementTween = _currentPawn.transform.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.OutCubic);
        }
    }


    public virtual void ResetCell()
    {
        if (_currentPawn != null)
        {
            _currentPawn = null;
        }
    }

    private async void OnMouseUp()
    {
        if (_currentPawn == null && BoardGameManager.Instance.CurrentPawn != null && GameManager.Instance.GameState == GameState.GAME)
        {
            _currentPawn = BoardGameManager.Instance.CurrentPawn;
            BoardGameManager.Instance.CurrentPawn = null;

            _currentPawn.transform.SetParent(_spawnPawnTransform);

            _currentPawn.BoxCollider.enabled = false;

            await _currentPawn.PickFeedbacks();

            await _currentPawn.transform.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();

            await _currentPawn.DropFeedbacks();

            SetCurrentPawn(_currentPawn, false);

            BoardGameManager.Instance.NewRound();
        }
    }

    public void UpdateSkin(SkinType newSkin)
    {
        _spriteRenderer.sprite = DataUtils.Instance.GetSpriteBySkinAndTier(newSkin, PawnTier.None, "cell");
    }
}
