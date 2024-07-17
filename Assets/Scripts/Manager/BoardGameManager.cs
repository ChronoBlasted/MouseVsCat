using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameManager : MonoSingleton<BoardGameManager>
{
    [SerializeField] Pawn _currentPawn;
    [SerializeField] Board _board;
    [SerializeField] Cell _newPawnCell, _nextPawnCell;
    [SerializeField] CellParadise _paradiseCell;
    [SerializeField] List<PawnProb> pawnProbs = new List<PawnProb>();

    public int mergeStreak = 0;

    Pawn _nextPawn;
    int _currentRound;

    public Board Board { get => _board; }
    public Cell NewPawnSpawn { get => _newPawnCell; }
    public Cell ParadiseCell { get => _paradiseCell; }
    public Pawn CurrentPawn { get => _currentPawn; set => _currentPawn = value; }

    public void Init()
    {
        UpdateNextPawn();

        NewRound();
    }

    public void ResetGame()
    {
        _board.ResetBoard();
        _currentRound = 0;
    }

    public void NewRound()
    {
        UpdateCurrentPawn();
        UpdateNextPawn();

        if (CheckIfNoMoreMove()) GameManager.Instance.UpdateStateToEnd();

        if (_currentRound != 0) ProfileManager.Instance.AddCoins(1);

        _currentRound++;
    }

    void UpdateCurrentPawn()
    {
        _currentPawn = _nextPawn;

        _currentPawn.BoxCollider.enabled = true;

        _newPawnCell.SetCurrentPawn(_currentPawn);

        _currentPawn.transform.localPosition = Vector3.zero;

        _currentPawn.Init(true);
    }

    void UpdateNextPawn()
    {
        _nextPawn = SpawnPawn(_nextPawnCell.SpawnPawnTransform);

        _nextPawnCell.SetCurrentPawn(_nextPawn);

        _nextPawn.BoxCollider.enabled = false;
    }

    Pawn SpawnPawn(Transform spawn)
    {
        float totalAmount = 0f;

        foreach (var prob in pawnProbs)
        {
            totalAmount += prob.prob;
        }

        float randNum = Random.Range(0, totalAmount);

        float cumulativePercentage = 0;

        Pawn currentPawn = null;

        for (int i = 0; i < pawnProbs.Count; i++)
        {
            cumulativePercentage += pawnProbs[i].prob;

            if (randNum <= cumulativePercentage)
            {
                currentPawn = PoolManager.Instance.SpawnFromPool("Pawn", spawn.position, spawn.rotation).GetComponent<Pawn>();

                currentPawn.transform.SetParent(spawn);

                currentPawn.PawnObject = DataUtils.Instance.GetPawnObjectByType(pawnProbs[i].type);

                currentPawn.Init();

                return currentPawn;
            }
        }

        return currentPawn;
    }

    public bool CheckIfNoMoreMove()
    {
        foreach (CellBoard cell in _board.AllCell)
        {
            if (cell.CurrentPawn == null) return false;
        }

        return true;
    }
}
