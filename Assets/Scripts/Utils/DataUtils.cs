using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils : MonoSingleton<DataUtils>
{
    [SerializeField] List<PawnObject> _pawnObjects;

    public PawnObject GetPawnObjectByType(PawnTier pawnType)
    {
        foreach (PawnObject obj in _pawnObjects)
        {
            if (obj.type == pawnType) return obj;
        }

        return null;
    }


    public PawnTier GetNextPawnTypeByPawnType(PawnTier type)
    {
        int currentEnumInt = (int)type;
        int nextEnumInt = currentEnumInt + 1;

        PawnTier nextEnumValue = (PawnTier)nextEnumInt;

        return nextEnumValue;
    }
}
