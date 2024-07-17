using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIAPData", menuName = "IAP/New IAP data", order = 1)]
public class IAPData : ScriptableObject
{
    public string ID;
    public string Title;
    public Sprite Preview;
    public int Quantity;
    public float Cost;
    public CurrencyType CurrencyType;
}
