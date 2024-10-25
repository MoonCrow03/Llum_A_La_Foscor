using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
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

    [Header("Scene Settings")]
    [SerializeField] private Scenes m_LevelCompleted;
    
    [Header("Audio")]
    public EventReference AudioEvent;
    public EventReference AudioEventWin;
    public EventReference AudioEventLose;

    private int m_CurrentStrikes;
    private int m_CurrentPoints;
    private TimeLimit m_TimeLimit;
    
    private FMOD.Studio.EventInstance m_AudioInstance;
    private FMOD.Studio.EventInstance m_AudioInstanceWin;
    private FMOD.Studio.EventInstance m_AudioInstanceLose;

    private void Start()
    {
        m_CurrentPoints = 0;
        m_CurrentStrikes = 0;

        if (m_GeodeMiniGameType == GeodeMiniGameType.TimeLimit)
        {
            m_TimeLimit = new TimeLimit(this);
            m_TimeLimit.StartTimer(m_Time, LoseGame);
        }
        
        m_AudioInstance = FMODUnity.RuntimeManager.CreateInstance(AudioEvent);
        m_AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(AudioEventWin);
        m_AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(AudioEventLose);
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

    private void WinGame()
    {
        Debug.Log("Geode minigame completed!");
        m_TimeLimit.StopTimer();
        GameManager.Instance.SetMiniGameCompleted(m_LevelCompleted);
        m_AudioInstanceWin.start();
        GameEvents.TriggerSetEndgameMessage("Felicitats!", true);
    }

    private void LoseGame()
    {
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
    }

    private void OnDisable()
    {
        GeodePart.OnStrike -= RegisterStrike;
        GeodePart.OnHit -= RegisterPoints;
    }

    private void OnDestroy()
    {
        m_AudioInstance.release();
        m_AudioInstanceWin.release();
        m_AudioInstanceLose.release();
    }
}
