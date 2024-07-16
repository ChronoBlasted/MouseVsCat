using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class CoinLayout : MonoBehaviour
{
    [SerializeField] TMP_Text coinAmount;

    int lastAmount;

    private void Start()
    {
        ProfileManager.Instance.OnCoinUpdate += UpdateCoinAmount;

        UpdateCoinAmount(ProfileManager.Instance.Coins);
    }

    void UpdateCoinAmount(int newAmount)
    {
        var split = coinAmount.text.Split(" ");

        lastAmount = int.Parse(split[0]);

        DOVirtual.Int(lastAmount, newAmount, 1f, x =>
        {
            coinAmount.text = x + " <sprite=0>"; ;
        });
    }
}
