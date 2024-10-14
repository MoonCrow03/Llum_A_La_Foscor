using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsPair : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private WordPairDrag m_WordAComponent;
    [SerializeField] private WordPairDrag m_WordBComponent;
    [SerializeField] private WordPairSlot m_SlotPairA;
    [SerializeField] private WordPairSlot m_SlotPairB;

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

    public bool IsPair()
    {
        if (m_SlotPairA.GetWord().Equals(m_SlotPairB.GetWordPair()))
        {
            return true;
        }

        return false;
    }
}
