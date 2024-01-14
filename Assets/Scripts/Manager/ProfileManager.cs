using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoSingleton<ProfileManager>
{
    [SerializeField] int _score, _highScore;

    public int Score { get => _score; }

    public void Init()
    {
        _highScore = SaveHandler.LoadValue("highScore", 0);
        ResetProfile();
    }

    public void ResetProfile()
    {
        _score = 0;
        PlayerPrefs.DeleteKey("highScore");
        UpdateScore(0);
    }

    public void UpdateScore(int amountToAdd)
    {
        _score += amountToAdd;

        if (_score < 0) _score = 0;

        if (_score > _highScore)
        {
            _highScore = _score;
            SaveHandler.SaveValue("highScore", _score);
        }

        UIManager.Instance.GamePanel.UpdateScore(_score);
    }
}