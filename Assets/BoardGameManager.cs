using BaseTemplate.Behaviours;
using UnityEngine;

public class BoardGameManager : MonoSingleton<BoardGameManager>
{
    [SerializeField] Board _board;
    [SerializeField] Transform _newPawnSpawn;

    int _currentRound;

    public Board Board { get => _board; }
    public Transform NewPawnSpawn { get => _newPawnSpawn; }

    public void Init()
    {
        ResetGame();

        NewRound();
    }

    void ResetGame()
    {
        _board.ResetBoard();
        _currentRound = 0;
    }

    public void NewRound()
    {
        PoolManager.Instance.SpawnFromPool("Cherry", _newPawnSpawn.position, _newPawnSpawn.rotation);

        _currentRound++;
    }
}
