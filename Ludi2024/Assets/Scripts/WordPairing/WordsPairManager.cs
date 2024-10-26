using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using Tutorial;
using UnityEngine;
using Utilities;

public class WordsPairManager : MonoBehaviour
{
    public static WordsPairManager Instance;

    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    [Header("Game Settings")]
    [SerializeField] private float m_Time;
    [SerializeField] private float m_PointMultiplier = 1.0f;

    [Header("Scene Settings")]
    [SerializeField] private TMPro.TextMeshProUGUI m_ClockText;
    [SerializeField] private bool m_IsTutorial;
    
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
        
        if (!m_IsTutorial)
        {
            m_TimeLimit = new TimeLimit(this);
            m_TimeLimit.StartTimer(m_Time, EndGameFailed);
        }
        
        m_CorrectPairCount = 0;
    }

    private void Update()
    {
        UpdateClockText();
    }
    
    private void UpdateClockText()
    {
        if (m_TimeLimit == null) return;
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

    private void StartTimer()
    {
        m_TimeLimit = new TimeLimit(this);
        m_TimeLimit.StartTimer(m_Time, EndGameFailed);
    }

    private void OnEnable()
    {
        WordPairSlot.OnWordDropped += CheckPairs;
        TutorialText.OnTutorialFinished += StartTimer;
    }

    private void OnDisable()
    {
        WordPairSlot.OnWordDropped -= CheckPairs;
        TutorialText.OnTutorialFinished -= StartTimer;
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
            
            m_TimeLimit.StopTimer();
            
            GameManager.Instance.Points += m_TimeLimit.GetPoints(m_PointMultiplier);

            int l_stars = 3;
            GameEvents.TriggerSetEndgameMessage("Felicitats!", true, l_stars);
        }
    }
    
    private void EndGameFailed()
    {
        if (m_IsGameCompleted) return;
        Debug.Log("Failed!");
        m_AudioInstanceLose.start();
        m_TimeLimit.StopTimer();
        GameEvents.TriggerSetEndgameMessage("Has perdut!", false, 0);
    }

    private void OnDestroy()
    {
        m_AudioInstanceWin.release();
        m_AudioInstanceLose.release();
    }
}
