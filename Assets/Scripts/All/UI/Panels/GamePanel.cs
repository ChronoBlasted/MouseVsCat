using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] TMP_Text _scoreTxt, _highscoreTxt;

    public override void Init()
    {
        base.Init();

        ProfileManager.Instance.OnScoreUpdate += UpdateScore;
        ProfileManager.Instance.OnHighScoreUpdate += UpdateHighscore;
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }

    public void OpenSettings()
    {
        UIManager.Instance.HandleOpenSettings();
    }

    public void OpenShop()
    {
        UIManager.Instance.HandleOpenShop();
    }

    public void UpdateScore(int newScore)
    {
        _scoreTxt.text = newScore.ToString();
    }

    public void UpdateHighscore(int newHighscore)
    {
        _highscoreTxt.text = "Highscore : " + newHighscore;
    }
}
