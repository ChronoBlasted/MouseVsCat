using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] TMP_Text _scoreTxt, _highScoreTxt;
    public override void Init()
    {
        base.Init();

        ProfileManager.Instance.OnScoreUpdate += UpdateScore;
        ProfileManager.Instance.OnHighScoreUpdate += UpdateHighScore;

        UpdateScore(ProfileManager.Instance.Score);
        UpdateHighScore(ProfileManager.Instance.HighScore);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }

    public void HandleReplayButton()
    {
        GameManager.Instance.ReloadScene();
    }

    void UpdateScore(int score)
    {
        _scoreTxt.text = "Score : " + score;
    }

    void UpdateHighScore(int highScore)
    {
        _highScoreTxt.text = "Highscore : " + highScore;
    }
}
