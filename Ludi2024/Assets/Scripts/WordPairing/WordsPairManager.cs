using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using UnityEngine;
using Utilities;

public class WordsPairManager : MonoBehaviour
{
    public static WordsPairManager Instance;

    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    [Header("Time Settings")] 
    [SerializeField] private float m_Time;
    
    [Header("Scene Settings")]
    [SerializeField] private Scenes m_LevelCompleted;
    
    [Header("Audio")]
    public EventReference m_AudioEventWin;
    public EventReference m_AudioEventLose;
    
    
    private int m_CorrectPairCount;
    private TimeLimit m_TimeLimit;
    private bool m_IsGameCompleted = false;
    
    private FMOD.Studio.EventInstance m_AudioInstanceWin;
    private FMOD.Studio.EventInstance m_AudioInstanceLose;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        m_AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(m_AudioEventWin);
        m_AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(m_AudioEventLose);
        
        m_TimeLimit = new TimeLimit(this);
        m_TimeLimit.StartTimer(m_Time, EndGameFailed);
        m_CorrectPairCount = 0;
    }

    private void OnEnable()
    {
        WordPairSlot.OnWordDropped += CheckPairs;
    }

    private void OnDisable()
    {
        WordPairSlot.OnWordDropped -= CheckPairs;
    }

    public void CheckPairs()
    {
        foreach (var t_pair in m_WordsSetters)
        {
            if (!t_pair.IsPair()) continue;

            t_pair.LockWords(true);
            m_CorrectPairCount++;
        }

        EndGame();
    }

    private void EndGame()
    {
        if(m_CorrectPairCount >= m_WordsSetters.Count)
        {
            m_IsGameCompleted = true;
            m_AudioInstanceWin.start();
            GameManager.Instance.SetMiniGameCompleted(m_LevelCompleted);
            GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
        }
    }
    
    private void EndGameFailed()
    {
        if (m_IsGameCompleted) return;
        Debug.Log("Failed!");
        m_AudioInstanceLose.start();
        GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
    }

    private void OnDestroy()
    {
        m_AudioInstanceWin.release();
        m_AudioInstanceLose.release();
    }
}
