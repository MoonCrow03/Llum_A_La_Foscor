using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordsPairManager : MonoBehaviour
{
    public static WordsPairManager Instance;

    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    private int m_CorrectPairCount;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
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
    }
}
