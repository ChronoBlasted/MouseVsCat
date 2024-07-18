using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;
    [SerializeField] CanvasGroup _canvasGroup;

    Tweener _fadeTween;

    public virtual void Init()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
    }

    public virtual void OpenPanel()
    {
        if (_fadeTween.IsActive()) _fadeTween.Kill();

        _fadeTween = _canvasGroup.DOFade(1, .2f).OnComplete(() =>
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }).SetUpdate(UpdateType.Normal, true);
    }

    public virtual void ClosePanel()
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        if (_fadeTween.IsActive()) _fadeTween.Kill();

        _fadeTween = _canvasGroup.DOFade(0, .2f)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            })
            .SetUpdate(UpdateType.Normal, true);
    }
}
