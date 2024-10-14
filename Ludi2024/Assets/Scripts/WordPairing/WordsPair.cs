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

    private ColorChanger m_ColorChanger;

    private void Awake()
    {
        m_ColorChanger = GetComponent<ColorChanger>();
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

    public bool IsPair()
    {
        if (!m_SlotPairA.HasWord()) return false;

        if(m_SlotPairB.GetWordPair() == null) return false;

        if (m_SlotPairA.GetWord().Equals(m_SlotPairB.GetWordPair()))
        {
            return true;
        }

        return false;
    }

    public void LockWords(bool p_lock)
    {
        m_ColorChanger.Correct();
        m_WordAComponent.Lock(p_lock);
        m_WordBComponent.Lock(p_lock);
    }
}
