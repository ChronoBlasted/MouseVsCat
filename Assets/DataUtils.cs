using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class DataUtils : MonoSingleton<DataUtils>
{
    [SerializeField] List<PawnObject> _pawnObjects;

    public PawnObject GetPawnObject(PawnType pawnType)
    {
        foreach (PawnObject obj in _pawnObjects)
        {
            if (obj.type == pawnType) return obj;
        }

        return null;
    }
}
