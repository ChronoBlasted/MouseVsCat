using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PawnShopLayout : MonoBehaviour
{
    [SerializeField] PawnTier _pawnTier;

    [SerializeField] Image _image;
    [SerializeField] TMP_Text _cost;

    int cost;

    private void Start()
    {
        SkinManager.Instance.OnSkinChange += UpdateSkin;

        UpdateSkin(SkinManager.Instance.currentSkin);

        cost = DataUtils.Instance.GetPawnObjectByType(_pawnTier).Cost;
        _cost.text = cost.ToString();
    }

    public void UpdateSkin(SkinType newSkin)
    {
        _image.sprite = DataUtils.Instance.GetSpriteBySkinAndTier(newSkin, _pawnTier);
    }

    public void HandleOnClick()
    {
        if (ProfileManager.Instance.AddCoins(-cost))
        {

        }
    }
}