using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    [SerializeField] GameObject _tab;

    public void HandleOnPress()
    {
        _tab.SetActive(true);
    }

    public void HandleOnReset()
    {
        _tab.SetActive(false);
    }
}
