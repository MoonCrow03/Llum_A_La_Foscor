using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPairs : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private WordPairDrag m_WordAComponent;
    [SerializeField] private WordPairDrag m_WordBComponent;

    private WordMixer m_WordMixer;

    private void Awake()
    {
        m_WordMixer = GetComponentInParent<WordMixer>();
    }

    public void SetWords(string p_wordA, string p_wordB)
    {
        m_WordAComponent.InitializeWords(p_wordA, p_wordB);
        m_WordBComponent.InitializeWords(p_wordB, p_wordA);
    }

    public void SetWordA(string p_word)
    {
        m_WordAComponent.SetWord(p_word);
    }

    public void SetWordB(string p_word)
    {
        m_WordBComponent.SetWord(p_word);
    }

    public bool CheckPair()
    {
        return true;
    }
}
