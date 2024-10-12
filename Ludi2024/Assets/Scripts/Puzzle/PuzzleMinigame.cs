using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

public class PuzzleMinigame : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int m_PuzzleSize;
    [SerializeField] private int m_PieceCounter;
    [SerializeField] private int m_SecondsToComplete;
    [SerializeField] private string m_WorldScene;
    
    private TimeLimit m_TimeLimit;
    private bool m_IsGameCompleted;

    private void Start()
    {
        SetPlayablePieces();
        SetUpTimer();
        m_PieceCounter = 0;
    }

    private void SetPlayablePieces()
    {
        List<PuzzlePiece> l_puzzlePieces = new List<PuzzlePiece>(gameObject.GetComponentsInChildren<PuzzlePiece>());

        foreach (var t_puzzlePiece in l_puzzlePieces)
        {
            if (t_puzzlePiece.IsSimple())
            {
                m_PuzzleSize++;
            }
        }
    }

    private void EndGame()
    {
        if (m_PieceCounter == m_PuzzleSize)
        {
            Debug.Log("Puzzle completed!");
            m_TimeLimit.StopTimer();
            BasicSceneChanger.ChangeScene(m_WorldScene);
        }
        
    }

    private void RegisterCorrectPiece()
    {
        m_PieceCounter++;

        EndGame();
    }

    private void SetUpTimer()
    {
        m_TimeLimit = new TimeLimit(this);
        m_TimeLimit.StartTimer(m_SecondsToComplete, RanOutOfTime);
    }

    private void RanOutOfTime()
    {
        Debug.Log("Ran out of time!");
        m_TimeLimit.StopTimer();
        BasicSceneChanger.ChangeScene(m_WorldScene);
    }

    private void OnEnable()
    {
        PuzzlePiece.OnPiecePlaced += RegisterCorrectPiece;
    }

    private void OnDisable()
    {
        PuzzlePiece.OnPiecePlaced -= RegisterCorrectPiece;
    }
}
