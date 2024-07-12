using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils : MonoSingleton<DataUtils>
{
    [SerializeField] List<PawnObject> _pawnObjects;

    public PawnObject GetPawnObjectByType(PawnType pawnType)
    {
        foreach (PawnObject obj in _pawnObjects)
        {
            if (obj.type == pawnType) return obj;
        }

        return null;
    }


    public PawnType GetNextPawnTypeByPawnType(PawnType type)
    {
        int currentEnumInt = (int)type;
        int nextEnumInt = currentEnumInt + 1;

        PawnType nextEnumValue = (PawnType)nextEnumInt;

        return nextEnumValue;
    }
}
