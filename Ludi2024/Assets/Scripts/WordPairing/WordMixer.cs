using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMixer : MonoBehaviour
{
    [Header("Word Settings")]
    [SerializeField] private List<string> m_WordsA;
    [SerializeField] private List<string> m_WordsB;

    public List<(string, string)> m_WordPair;

    [Header("Components")]
    [SerializeField] private List<WordsPair> m_WordsSetters;

    private void Awake()
    {
        m_WordPair = new List<(string, string)>();

        if (m_WordsA.Count != m_WordsB.Count && m_WordsA.Count != m_WordsSetters.Count)
        {
            Debug.LogError("WordMixer: WordsA, WordsB and WordsSetters must have the same size.");
        }

        for (int i = 0; i < m_WordsSetters.Count; i++)
        {
            m_WordPair.Add((m_WordsA[i], m_WordsB[i]));
            m_WordsSetters[i].SetBothWords(m_WordPair[i], i);
        }

        MixWords();
    }

    private void MixWords()
    {
        // Step 1: Create a list of word pairs
        List<(string wordA, string wordB)> l_wordPairs = new List<(string wordA, string wordB)>();

        for (int i = 0; i < m_WordsA.Count; i++)
        {
            l_wordPairs.Add((m_WordsA[i], m_WordsB[i]));
        }

        // Step 2: Shuffle the list of word pairs with a constraint that wordB does not stay in the same index
        List<(string wordA, string wordB)> l_shuffledWordPairs = ShuffleWithConstraint(l_wordPairs);

        // Step 3: Reassign the shuffled word pairs back to WordSetters
        for (int i = 0; i < m_WordsSetters.Count; i++)
        {
            var l_pair = l_shuffledWordPairs[i];

            int l_wordId = 0;

            foreach (var t_setter in m_WordsSetters)
            {
                if (t_setter.GetWordPair().Equals(l_pair))
                {
                    l_wordId = t_setter.GetWordId();
                }
            }

            m_WordsSetters[i].SetWordB(l_pair, l_wordId);
        }
    }

// Shuffle word pairs while ensuring that no wordB remains in the same position as it was initially
private List<(string wordA, string wordB)> ShuffleWithConstraint(List<(string wordA, string wordB)> p_wordPairs)
{
    List<(string wordA, string wordB)> shuffledPairs = new List<(string wordA, string wordB)>(p_wordPairs);
    int l_attempts = 0;

    do
    {
        // Step 1: Shuffle the list
        for (int i = shuffledPairs.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            var temp = shuffledPairs[i];

            shuffledPairs[i] = shuffledPairs[randomIndex];
            shuffledPairs[randomIndex] = temp;
        }

        // Step 2: Check if any wordB coincides with its original position
        l_attempts++;
    }
    while (HasSameIndexElements(p_wordPairs, shuffledPairs) && l_attempts < 100);

    if (l_attempts >= 100)
    {
        Debug.LogError("Failed to shuffle without matching positions after 100 attempts.");
    }

    return shuffledPairs;
}

// Helper method to check if any wordB in shuffledPairs is at the same index as in originalPairs
private bool HasSameIndexElements(List<(string wordA, string wordB)> originalPairs, List<(string wordA, string wordB)> shuffledPairs)
{
    for (int i = 0; i < originalPairs.Count; i++)
    {
        if (originalPairs[i].wordB.Equals(shuffledPairs[i].wordB))
        {
            return true; // wordB is in the same index as in the original list
        }
    }
    return false;
}
}
