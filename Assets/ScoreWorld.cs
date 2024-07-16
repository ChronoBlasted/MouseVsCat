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
        _scoreTxt.text = newScore.ToString();
    }

    public void UpdateHighscore(int newHighscore)
    {
        _highscoreTxt.text = "Highscore : " + newHighscore;
    }

}
