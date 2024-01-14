using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] TMP_Text _scoreText;

    public override void Init()
    {
        base.Init();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }

    public void StartGame()
    {
    }

    public void UpdateScore(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
