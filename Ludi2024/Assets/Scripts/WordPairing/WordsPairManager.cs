using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    
    private int m_CorrectPairCount;
    private TimeLimit m_TimeLimit;
    private bool m_IsGameCompleted = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
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
            Debug.Log("Finished!");
            GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
            GameManager.Instance.SetMiniGameCompleted(m_LevelCompleted);
        }
    }
    
    private void EndGameFailed()
    {
        if (m_IsGameCompleted) return;
        Debug.Log("Failed!");
        GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
    }
}
