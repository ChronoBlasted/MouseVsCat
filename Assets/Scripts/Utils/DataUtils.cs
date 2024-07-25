using BaseTemplate.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils : MonoSingleton<DataUtils>
{
    [SerializeField] List<PawnObject> _pawnObjects;
    [SerializeField] List<SkinData> _allSkins;

    public PawnObject GetPawnObjectByTier(PawnTier pawnType)
    {
        foreach (PawnObject obj in _pawnObjects)
        {
            if (obj.tier == pawnType) return obj;
        }

        return null;
    }


    public PawnTier GetNextPawnTierByPawnTier(PawnTier type)
    {
        PawnTier[] values = (PawnTier[])Enum.GetValues(typeof(PawnTier));

        int currentEnumInt = (int)type;
        int nextEnumInt = currentEnumInt + 1;

        if (nextEnumInt >= values.Length)
        {
            nextEnumInt = 1;
        }

        PawnTier nextEnumValue = (PawnTier)nextEnumInt;

        return nextEnumValue;
    }

    public Sprite GetSpriteBySkinAndTier(SkinType skinType, PawnTier tier, string other = "")
    {
        SkinData skinData = null;

        foreach (var skin in _allSkins)
        {
            if (skin.SkinType == skinType)
            {
                skinData = skin;
                break;
            }
        }

        switch (tier)
        {
            case PawnTier.Tier1:
                return skinData.tier1;
            case PawnTier.Tier2:
                return skinData.tier2;
            case PawnTier.Tier3:
                return skinData.tier3;
            case PawnTier.Tier4:
                return skinData.tier4;
            case PawnTier.Tier5:
                return skinData.tier5;
            case PawnTier.Tier6:
                return skinData.tier6;
            case PawnTier.Tier7:
                return skinData.tier7;
            case PawnTier.Tier8:
                return skinData.tier8;
            case PawnTier.Tier9:
                return skinData.tier9;
            case PawnTier.Joker1up:
                return skinData.joker1up;
            case PawnTier.JokerChoose:
                return skinData.jokerChoose;
            case PawnTier.DestroyPawn:
                return skinData.destroyPawn;
            case PawnTier.SwitchCellPawn:
                return skinData.switchPawn;
        }

        if (other == "background")
        {
            return skinData.background;
        }
        else if (other == "cell")
        {
            return skinData.cell;
        }

        return null;
    }
}
