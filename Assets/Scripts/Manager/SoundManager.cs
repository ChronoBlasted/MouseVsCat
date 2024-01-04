using BaseTemplate.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioSource _musicSource, _effectSource;
    [SerializeField] AudioClip _menuMusic, _gameMusic, _endMusic;
    [SerializeField] List<AudioString> _effectList;

    Dictionary<string, List<AudioClip>> _effectDict = new Dictionary<string, List<AudioClip>>();
    List<AudioClip> _effectToPlay;

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        InitDictionnary();
    }

    private void InitDictionnary()
    {
        foreach (AudioString camItem in _effectList)
        {
            _effectDict.Add(camItem.AudioName, camItem.AudioClip);
        }
    }

    public void PlaySound(string audioClip)
    {
        _effectDict.TryGetValue(audioClip, out _effectToPlay);
        int indexSound = Random.Range(0, _effectToPlay.Count);
        _effectSource.PlayOneShot(_effectToPlay[indexSound]);
    }

    void PlayMusic(AudioClip audioClip)
    {
        if (_musicSource.clip == audioClip) return;
        _musicSource.clip = audioClip;
        _musicSource.Play();
    }

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                HandleMenu();
                break;
            case GameState.GAME:
                HandleGame();
                break;
            case GameState.PAUSE:
                HandlePause();
                break;
            case GameState.END:
                HandleEnd();
                break;
            case GameState.WAIT:
                HandleWait();
                break;
            default:
                break;
        }
    }

    void HandleMenu()
    {
        PlayMusic(_menuMusic);
    }
    void HandleGame()
    {
        PlayMusic(_gameMusic);
    }
    void HandlePause()
    {
    }
    void HandleEnd()
    {
        PlayMusic(_endMusic);
    }
    void HandleWait()
    {

    }
}

[Serializable]
public class AudioString
{
    public string AudioName;
    public List<AudioClip> AudioClip;
}
