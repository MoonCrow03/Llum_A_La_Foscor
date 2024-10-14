using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordsPairManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    private int m_CorrectPairCount;

    private void Start()
    {
        m_CorrectPairCount = 0;
    }

    private void Update()
    {
        CheckPairs();
    }

    private void OnEnable()
    {
        WordPairSlot.OnCheckPairs += CheckPairs;
    }

    private void OnDisable()
    {
        WordPairSlot.OnCheckPairs -= CheckPairs;
    }

    private void CheckPairs()
    {
        foreach (var t_pair in m_WordsSetters)
        {
            if (!t_pair.IsPair()) continue;

            t_pair.LockWords(true);
            m_CorrectPairCount++;
        }
    }
}
