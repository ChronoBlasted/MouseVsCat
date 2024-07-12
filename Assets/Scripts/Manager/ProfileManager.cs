using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoSingleton<ProfileManager>
{
    [SerializeField] int _score, _highScore, _coins;

    public int Score { get => _score; }
    public int HighScore { get => _highScore; }
    public int Coins { get => _coins; }

    public event Action OnCoinUpdate;
    public event Action<int> OnScoreUpdate;
    public event Action<int> OnHighScoreUpdate;

    public void Init()
    {
        _highScore = SaveHandler.LoadValue("highScore", 0);
        _coins = SaveHandler.LoadValue("coins", 0);

        OnScoreUpdate?.Invoke(_score);
        OnHighScoreUpdate?.Invoke(_highScore);
    }

    public void ResetProfile()
    {
        _score = 0;
        PlayerPrefs.DeleteKey("highScore");
        PlayerPrefs.DeleteKey("coins");
        UpdateScore(0);
    }

    public void UpdateScore(int amountToAdd)
    {
        _score += amountToAdd;

        if (_score < 0) _score = 0;

        if (_score > _highScore)
        {
            _highScore = _score;
            OnHighScoreUpdate?.Invoke(_highScore);

            SaveHandler.SaveValue("highScore", _score);
        }

        OnScoreUpdate?.Invoke(_score);
    }

    public void AddCoins(int amountToAdd)
    {
        _coins += amountToAdd;

        if (_coins < 0) _coins = 0;

        OnCoinUpdate?.Invoke();
    }
}