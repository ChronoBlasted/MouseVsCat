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

    private void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            ChangeSkin(SkinType.BALL);
        }

        if (Input.GetKey(KeyCode.F))
        {
            ChangeSkin(SkinType.FRUIT);
        }
    }
}

public enum SkinType
{
    FRUIT,
    BALL,
    GOLDEN
}

