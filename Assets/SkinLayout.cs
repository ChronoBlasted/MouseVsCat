using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinLayout : MonoBehaviour
{
    [SerializeField] GameObject _buyLayout;
    [SerializeField] Image _previewImage, _equipImage;
    [SerializeField] TMP_Text _costTxt;
    [SerializeField] SkinData _skinData;

    [SerializeField] Sprite _equippedSprite, _canBeEquippedSprite;

    bool isPossesed;
    bool isEquipped;

    private void Start()
    {
        isPossesed = SaveHandler.LoadValue(_skinData.SkinType.ToString(), "") == "owned";
        isEquipped = SkinManager.Instance.currentSkin == _skinData.SkinType;

        _previewImage.sprite = _skinData.tier9;
        _costTxt.text = _skinData.cost.ToString();

        UpdateData();
    }

    public void HandleOnClick()
    {
        if (SaveHandler.LoadValue(_skinData.SkinType.ToString(), "") == "owned")
        {
            SkinManager.Instance.ChangeSkin(_skinData.SkinType);
        }
        else
        {
            if (ProfileManager.Instance.AddHardCurrency(-_skinData.cost))
            {
                SaveHandler.SaveValue(_skinData.SkinType.ToString(), "owned");

                SkinManager.Instance.ChangeSkin(_skinData.SkinType);
            }
            else
            {
                Debug.Log("FAIRE UN MESSAGE D'�rreur / feedbacks d'erreur");
            }
        }
    }

    public void UpdateData()
    {
        if (isPossesed)
        {
            _buyLayout.SetActive(false);

            if (isEquipped)
            {
                _equipImage.sprite = _equippedSprite;
            }
            else
            {
                _equipImage.sprite = _canBeEquippedSprite;
            }
        }
    }

    public void Equip()
    {
        isEquipped = true;

        UpdateData();
    }

    public void Unequip()
    {
        isEquipped = false;

        UpdateData();
    }
}