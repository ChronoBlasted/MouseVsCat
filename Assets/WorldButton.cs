using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    enum ButtonType { NONE, SCALE }

    [SerializeField] ButtonType _type;

    [SerializeField] Transform _transform;

    [SerializeField] float _timeOfScale = .2f;

    [SerializeField] UnityEvent onClickAction;

    Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameState.GAME) return;

        //AudioManager.Instance.PlaySound("Button");

        switch (_type)
        {
            case ButtonType.SCALE:
                ScaleDownElement();
                break;
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.GameState != GameState.GAME) return;

        switch (_type)
        {
            case ButtonType.SCALE:
                ResetScaleElement();
                break;
        }

        onClickAction?.Invoke();
    }

    private void ScaleDownElement() => _transform.DOScale(baseScale - new Vector3(.05f, .05f, .05f), _timeOfScale).SetEase(Ease.OutExpo);
    private void ResetScaleElement() => _transform.DOScale(baseScale, _timeOfScale).SetEase(Ease.OutExpo);
}
