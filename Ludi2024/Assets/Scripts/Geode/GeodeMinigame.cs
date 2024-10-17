using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("World Scene")]
    [SerializeField] private string m_WorldScene;

    private int m_CurrentStrikes;
    private int m_CurrentPoints;
    private TimeLimit m_TimeLimit;

    private void Start()
    {
        m_CurrentPoints = 0;
        m_CurrentStrikes = 0;

        if (m_GeodeMiniGameType == GeodeMiniGameType.TimeLimit)
        {
            m_TimeLimit = new TimeLimit(this);
            m_TimeLimit.StartTimer(m_Time, LoseGame);
        }
    }

    private void RegisterPoints()
    {
        m_CurrentPoints++;

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
        GameManager.Instance.SetMinigameCompleted("GeodeMinigame");
        BasicSceneChanger.ChangeScene(m_WorldScene);
    }

    private void LoseGame()
    {
        Debug.Log("Geode minigame failed!");

        if (m_GeodeMiniGameType == GeodeMiniGameType.TimeLimit)
            m_TimeLimit.StopTimer();
        BasicSceneChanger.ChangeScene(m_WorldScene);
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
    
}
