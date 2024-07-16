using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWorld : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreTxt, _highscoreTxt;

    void Start()
    {
        ProfileManager.Instance.OnScoreUpdate += UpdateScore;
        ProfileManager.Instance.OnHighScoreUpdate += UpdateHighscore;

        UpdateScore(ProfileManager.Instance.Score);
        UpdateHighscore(ProfileManager.Instance.HighScore);
    }

    public void UpdateScore(int newScore)
    {
        var lastAmount = int.Parse(_scoreTxt.text);

        DOVirtual.Int(lastAmount, newScore, 1f, x =>
        {
            _scoreTxt.text = x.ToString();
        });
    }

    public void UpdateHighscore(int newHighscore)
    {
        var split = _highscoreTxt.text.Split(" ");

        var lastAmount = int.Parse(split[2]);

        DOVirtual.Int(lastAmount, newHighscore, 1f, x =>
        {
            _highscoreTxt.text = "Highscore : " + x;
        });
    }

}
