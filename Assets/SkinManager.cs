using BaseTemplate.Behaviours;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkinManager : MonoSingleton<SkinManager>
{
    [SerializeField] SpriteRenderer background;

    public event Action<SkinType> OnSkinChange;

    public SkinType currentSkin;

    public void Init()
    {
        SaveHandler.SaveValue(SkinType.FRUIT.ToString(), "owned");

        currentSkin = (SkinType)SaveHandler.LoadValue("currentSkin", 0);

        ChangeSkin(currentSkin);
    }

    public void ChangeSkin(SkinType newSkinType)
    {
        currentSkin = newSkinType;

        background.sprite = DataUtils.Instance.GetSpriteBySkinAndTier(currentSkin, PawnTier.None, "background");

        SaveHandler.SaveValue("currentSkin", (int)currentSkin);

        OnSkinChange?.Invoke(currentSkin);
    }
}

public enum SkinType
{
    FRUIT,
    BALL,
    GOLDEN
}

