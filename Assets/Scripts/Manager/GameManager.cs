using BaseTemplate.Behaviours;
using DG.Tweening;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { GAME, MENU, END }

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameState> OnGameStateChanged;

    GameState _gameState;
    public GameState GameState { get => _gameState; }

    private async void Awake()
    {
        Time.timeScale = 1;

        PoolManager.Instance.Init();

        SkinManager.Instance.Init();

        ProfileManager.Instance.Init();

        UIManager.Instance.Init();

        BoardGameManager.Instance.Init();

        await Task.Delay(1);

        CameraManager.Instance.Init();

        UpdateGameState(GameState.GAME);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void UpdateGameState(GameState newState)
    {
        _gameState = newState;

        switch (_gameState)
        {
            case GameState.GAME:
                HandleGame();
                break;
            case GameState.END:
                HandleEnd();
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(_gameState);
    }

    void HandleGame()
    {
    }

    void HandleEnd()
    {
    }

    public void UpdateStateToGame() => UpdateGameState(GameState.GAME);
    public void UpdateStateToEnd() => UpdateGameState(GameState.END);

    public void ReloadScene()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitApp() => Application.Quit();
}