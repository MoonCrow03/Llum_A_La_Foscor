using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Tutorial;
using UnityEngine;
using Utilities;

public class GeodeMinigame : MonoBehaviour
{
    private enum GeodeMiniGameType{
        Basic,
        TimeLimit,
    }

    [Header("Geode Settings")]
    [SerializeField] private GeodeMiniGameType m_GeodeMiniGameType;
    [SerializeField] private float m_Time = 20.0f;
    [SerializeField] private int m_MaxStrikes = 2;
    [SerializeField] private int m_PointsToWin = 3;
    [SerializeField] private bool m_IsTutorial;

    [Header("Scene Settings")]
    [SerializeField] private Scenes m_LevelCompleted;
    [SerializeField] private TMPro.TextMeshProUGUI m_clockTimeLeft;
    
    [Header("Audio")]
    public EventReference AudioEvent;
    public EventReference AudioEventWin;
    public EventReference AudioEventLose;

    private int m_CurrentStrikes;
    private int m_CurrentPoints;
    private TimeLimit m_TimeLimit;
    private bool m_GameStarted = false;
    private bool m_IsGameCompleted = false;
    
    private FMOD.Studio.EventInstance m_AudioInstance;
    private FMOD.Studio.EventInstance m_AudioInstanceWin;
    private FMOD.Studio.EventInstance m_AudioInstanceLose;

    private void Start()
    {
        m_CurrentPoints = 0;
        m_CurrentStrikes = 0;

        if (m_GeodeMiniGameType == GeodeMiniGameType.TimeLimit && !m_IsTutorial)
        {
            m_TimeLimit = new TimeLimit(this);
            m_TimeLimit.StartTimer(m_Time, LoseGame);
        }
        
        m_AudioInstance = FMODUnity.RuntimeManager.CreateInstance(AudioEvent);
        m_AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(AudioEventWin);
        m_AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(AudioEventLose);
    }

    private void Update()
    {
        if (m_GeodeMiniGameType != GeodeMiniGameType.TimeLimit) return;
        UpdateClock();
    }

    private void UpdateClock()
    {
        if (m_TimeLimit == null) return;
        if (m_TimeLimit.GetTimeRemaining() <= 0)
        {
            m_clockTimeLeft.text = "00:00";
        }
        else
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(m_TimeLimit.GetTimeRemaining());
            m_clockTimeLeft.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }

    private void RegisterPoints()
    {
        m_CurrentPoints++;
        m_AudioInstance.start();

        if (m_CurrentPoints == m_PointsToWin)
        {
            WinGame();
        }
    }

    private void RegisterStrike()
    {
        m_CurrentStrikes++;
        
        if (m_CurrentStrikes == m_MaxStrikes)
        {
            LoseGame();
        }
    }
    
    private void StartTimer()
    {
        m_TimeLimit = new TimeLimit(this);
        m_TimeLimit.StartTimer(m_Time, LoseGame);
    }

    private void WinGame()
    {
        m_IsGameCompleted = true;
        m_TimeLimit.StopTimer();
        GameManager.Instance.SetMiniGameCompleted(m_LevelCompleted);
        m_AudioInstanceWin.start();
        GameEvents.TriggerSetEndgameMessage("Felicitats!", true);
    }

    private void LoseGame()
    {
        if (m_IsGameCompleted) return;
        Debug.Log("Geode minigame failed!");
        m_AudioInstanceLose.start();
        if (m_GeodeMiniGameType == GeodeMiniGameType.TimeLimit)
            m_TimeLimit.StopTimer();

        GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
    }

    private void OnEnable()
    {
        GeodePart.OnStrike += RegisterStrike;
        GeodePart.OnHit += RegisterPoints;
        TutorialText.OnTutorialFinished += StartTimer;
    }

    private void OnDisable()
    {
        GeodePart.OnStrike -= RegisterStrike;
        GeodePart.OnHit -= RegisterPoints;
        TutorialText.OnTutorialFinished -= StartTimer;
    }

    private void OnDestroy()
    {
        m_AudioInstance.release();
        m_AudioInstanceWin.release();
        m_AudioInstanceLose.release();
    }
}
