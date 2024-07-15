using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoSingleton<SkinManager>
{
    [SerializeField] List<SkinData> allSkins;

    public event Action<SkinType> OnSkinChange;

    SkinType currentSkin;

    public void Init()
    {
        currentSkin = SaveHandler.LoadValue("currentSkin", (SkinType)0);

        ChangeSkin(currentSkin);
    }

    public void ChangeSkin(SkinType newSkinType)
    {
        // Change Background 
        // Change Cells

        OnSkinChange?.Invoke(newSkinType);
    }
}

public enum SkinType
{
    FRUIT,
    SUGAR,
    GOLDEN
}

