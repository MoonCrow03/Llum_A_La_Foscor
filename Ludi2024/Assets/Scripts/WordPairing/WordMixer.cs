using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMixer : MonoBehaviour
{
    [Header("Word Settings")]
    [SerializeField] private List<string> m_WordsA;
    [SerializeField] private List<string> m_WordsB;

    [Header("Components")]
    [SerializeField] private List<WordPairs> m_WordsSetters;

    private void Start()
    {
        if (m_WordsA.Count != m_WordsB.Count && m_WordsA.Count != m_WordsSetters.Count)
        {
            Debug.LogError("WordMixer: WordsA, WordsB and WordsSetters must have the same size.");
        }

        for (int i = 0; i < m_WordsSetters.Count; i++)
        {
            m_WordsSetters[i].SetWords(m_WordsA[i], m_WordsB[i]);
        }

        MixWords();
    }

    private void MixWords()
    {
        List<string> l_wordsA = new List<string>(m_WordsA);
        List<string> l_wordsB = new List<string>(m_WordsB);

        // Shuffle List B with constraints
        ShuffleWithConstraint(l_wordsA, l_wordsB);

        // Output shuffled lists
        Debug.Log("List A: " + string.Join(", ", l_wordsA));
        Debug.Log("List B: " + string.Join(", ", l_wordsB));

        for (int i = 0; i < m_WordsSetters.Count; i++)
        {
            m_WordsSetters[i].SetWordA(l_wordsA[i]);
            m_WordsSetters[i].SetWordB(l_wordsB[i]);
        }
    }

    // Shuffle List B with the constraint that B[i] != A[i]
    private void ShuffleWithConstraint<T>(List<T> p_AList, List<T> p_BList)
    {
        int l_attempts = 0;

        do
        {
            // Shuffle list p_BList
            for (int i = p_BList.Count - 1; i > 0; i--)
            {
                int t_randomIndex = Random.Range(0, p_BList.Count);
                T t_temp = p_BList[i];
                p_BList[i] = p_BList[t_randomIndex];
                p_BList[t_randomIndex] = t_temp;
            }

            // Check if any elements are at the same index in both lists
            l_attempts++;
        } while (HasSameIndexElements(p_AList, p_BList) && l_attempts < 100);

        if (l_attempts >= 100)
        {
            Debug.LogError("Failed to shuffle after 100 attempts. List p_BList might still have some coinciding elements.");
        }
    }

    // Helper method to check if any element in A is in the same index as in B
    private bool HasSameIndexElements<T>(List<T> p_AList, List<T> p_BList)
    {
        for (int i = 0; i < p_AList.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(p_AList[i], p_BList[i]))
            {
                return true; // Same element at the same index
            }
        }
        return false;
    }
}
