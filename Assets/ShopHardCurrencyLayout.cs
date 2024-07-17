using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopHardCurrencyLayout : MonoBehaviour
{
    [SerializeField] TMP_Text _title, _price;
    [SerializeField] Image _preview;

    [SerializeField] IAPData _data;

    private void Start()
    {
        _title.text = _data.Title;
        _price.text = _data.Cost.ToString();

        _preview.sprite = _data.Preview;
    }

    public void HandleOnClick()
    {
        // Faire 
    }
}
