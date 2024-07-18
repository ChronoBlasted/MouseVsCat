using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinData", menuName = "Skin/New skin data", order = 1)]
public class SkinData : ScriptableObject
{
    public SkinType SkinType;
    public int cost;

    public Sprite background;
    public Sprite cell;

    public Sprite tier1;
    public Sprite tier2;
    public Sprite tier3;
    public Sprite tier4;
    public Sprite tier5;
    public Sprite tier6;
    public Sprite tier7;
    public Sprite tier8;
    public Sprite tier9;

    public Sprite joker1up;
}
