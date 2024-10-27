using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using Tutorial;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
    [SerializeField] private float m_PointsMultiplier = 1.0f;
    
    [Header("Audio")]
    [SerializeField] private EventReference m_AudioEventWin;
    [SerializeField] private EventReference m_AudioEventLose;

    [Header("Debug")]
    [SerializeField] private int m_PieceCounter;
    [SerializeField] private int m_PuzzleSize;
    
    [Header("Scene Settings")]
    [SerializeField] private TextMeshProUGUI m_ClockText;

    private TimeLimit m_TimeLimit;
    private bool m_GameStarted = false;
    private bool m_IsGameCompleted = false;
    
    private FMOD.Studio.EventInstance m_AudioInstanceWin;
    private FMOD.Studio.EventInstance m_AudioInstanceLose;
    
    private void Start()
    {
        SetPlayablePieces();

        m_GameStarted = false;
    }

    private void Update()
    {
        if(!m_GameStarted) return;

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

    private void StartGame()
    {
        m_GameStarted = true;

        if (m_PuzzleMiniGameType == PuzzleMiniGameType.TimeLimit)
        {
            SetUpTimer();
        }
    }
    private void EndGame()
    {
        if (m_PieceCounter != m_PuzzleSize) return;
        
        Debug.Log("Puzzle completed!");
        if (m_PuzzleMiniGameType == PuzzleMiniGameType.TimeLimit)
            m_TimeLimit.StopTimer();
        
        m_IsGameCompleted = true;
        
        GameManager.Instance.Points += m_TimeLimit.GetPoints(m_PointsMultiplier);

        int l_stars = m_TimeLimit.GetNumOfStars();
        m_AudioInstanceWin.start();
        GameEvents.TriggerSetEndgameMessage("Felicitats!", true, l_stars);
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
        m_AudioInstanceLose.start();
        GameEvents.TriggerSetEndgameMessage("T'has quedat sense temps!", false, 0);
    }

    private void OnEnable()
    {
        PuzzlePiece.OnPiecePlaced += RegisterCorrectPiece;
        TutorialText.OnTutorialFinished += StartGame;
    }

    private void OnDisable()
    {
        PuzzlePiece.OnPiecePlaced -= RegisterCorrectPiece;
        TutorialText.OnTutorialFinished -= StartGame;
    }

    private void OnDestroy()
    {
        m_AudioInstanceLose.release();
        m_AudioInstanceWin.release();
    }
}
