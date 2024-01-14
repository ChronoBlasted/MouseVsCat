using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameManager : MonoSingleton<BoardGameManager>
{
    [SerializeField] Board _board;
    [SerializeField] Transform _newPawnSpawn;
    [SerializeField] List<PawnProb> pawnProbs = new List<PawnProb>();

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
        float totalAmount = 0f;

        foreach (var prob in pawnProbs)
        {
            totalAmount += prob.prob;
        }

        float randNum = Random.Range(0, totalAmount);

        float cumulativePercentage = 0;

        for (int i = 0; i < pawnProbs.Count; i++)
        {
            cumulativePercentage += pawnProbs[i].prob;

            if (randNum <= cumulativePercentage)
            {
                PoolManager.Instance.SpawnFromPool(pawnProbs[i].type.ToString(), _newPawnSpawn.position, _newPawnSpawn.rotation);
                break;
            }
        }

        _currentRound++;
    }
}
