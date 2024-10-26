using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

public class PuzzleMinigame : MonoBehaviour
{
    private enum PuzzleMiniGameType
    {
        Basic,
        TimeLimit
    }

    [Header("Puzzle Settings")]
    [SerializeField] private PuzzleMiniGameType m_PuzzleMiniGameType;
    [SerializeField] private float m_SecondsToComplete;

    [Header("Debug")]
    [SerializeField] private int m_PieceCounter;
    [SerializeField] private int m_PuzzleSize;
    
    [Header("Scene Settings")]
    [SerializeField] private Scenes m_LevelCompleted;
    [SerializeField] private TextMeshProUGUI m_ClockText;

    private TimeLimit m_TimeLimit;
    private bool m_IsGameCompleted = false;
    
    private void Start()
    {
        SetPlayablePieces();

        if (m_PuzzleMiniGameType == PuzzleMiniGameType.TimeLimit)
        {
            SetUpTimer();
        }
    }

    private void Update()
    {
        if (m_PuzzleMiniGameType != PuzzleMiniGameType.TimeLimit) return;
        
        UpdateClockText();
    }

    private void UpdateClockText()
    {
        if (m_TimeLimit.GetTimeRemaining() <= 0)
        {
            m_ClockText.text = "00:00";
        }
        else
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(m_TimeLimit.GetTimeRemaining());
            m_ClockText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }

    private void SetPlayablePieces()
    {
        m_PuzzleSize = 0;
        m_PieceCounter = 0;

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
        if (m_PieceCounter != m_PuzzleSize) return;
        
        Debug.Log("Puzzle completed!");
        if (m_PuzzleMiniGameType == PuzzleMiniGameType.TimeLimit)
            m_TimeLimit.StopTimer();
        
        m_IsGameCompleted = true;
        GameManager.Instance.SetMiniGameCompleted(m_LevelCompleted);
        GameEvents.TriggerSetEndgameMessage("Trencaclosques completat!", true);
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
        if (m_IsGameCompleted) return;
        Debug.Log("Ran out of time!");
        m_TimeLimit.StopTimer();
        GameEvents.TriggerSetEndgameMessage("T'has quedat!", false);
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
