using BaseTemplate.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoSingleton<ProfileManager>
{
    [SerializeField] int _score, _highScore, _coins, _hardCurrency;

    public int Score { get => _score; }
    public int HighScore { get => _highScore; }
    public int Coins { get => _coins; }
    public int HardCurrency { get => _hardCurrency; }

    public event Action<int> OnCoinUpdate;
    public event Action<int> OnHardCurrencyUpdate;
    public event Action<int> OnScoreUpdate;
    public event Action<int> OnHighScoreUpdate;

    public void Init()
    {
        _highScore = SaveHandler.LoadValue("highScore", 0);
        _coins = SaveHandler.LoadValue("coins", 0);
        _hardCurrency = SaveHandler.LoadValue("hardCurrency", 0);

        OnScoreUpdate?.Invoke(_score);
        OnHighScoreUpdate?.Invoke(_highScore);
    }

    public void ResetProfile()
    {
        _score = 0;
        UpdateScore(0);
        PlayerPrefs.DeleteKey("coins");
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

    public bool AddCoins(int amountToAdd)
    {
        if (_coins + amountToAdd < 0)
        {
            return false;
        }

        _coins += amountToAdd;

        OnCoinUpdate?.Invoke(_coins);

        SaveHandler.SaveValue("coins", _coins);

        return true;
    }

    public bool AddHardCurrency(int amountToAdd)
    {
        if (_hardCurrency + amountToAdd < 0)
        {
            return false;
        }

        _hardCurrency += amountToAdd;

        OnHardCurrencyUpdate?.Invoke(_hardCurrency);

        SaveHandler.SaveValue("hardCurrency", _hardCurrency);

        return true;
    }

    private void OnDestroy()
    {
        ResetProfile();
    }
}