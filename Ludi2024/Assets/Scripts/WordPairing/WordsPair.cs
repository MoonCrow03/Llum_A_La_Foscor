using System;
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

    public bool IsLocked;

    private ColorChanger m_ColorChanger;

    private void Awake()
    {
        m_ColorChanger = GetComponent<ColorChanger>();
    }

    public void SetBothWords((string p_wordA, string p_wordB) p_wordPair, int p_index)
    {
        m_WordAComponent.SetWords(p_wordPair, p_index);
        m_WordBComponent.SetWords(p_wordPair, p_index);
    }

    public void SetWordA((string p_wordA, string p_wordB) p_wordPair, int p_index)
    {
        m_WordAComponent.SetWords(p_wordPair, p_index);
    }

    public void SetWordB((string p_wordA, string p_wordB) p_wordPair, int p_index)
    {
        m_WordBComponent.SetWords(p_wordPair, p_index);
    }

    public int GetWordId()
    {
        return transform.GetSiblingIndex();
    }

    public (string, string) GetWordPair()
    {
        return m_WordAComponent.GetWordPair();
    }

    public bool IsPair()
    {
        int A = m_SlotPairA.GetWordId();
        int B = m_SlotPairB.GetWordId();

        return m_SlotPairA.GetWordId().Equals(m_SlotPairB.GetWordId());
    }

    public void LockWords(bool p_lock)
    {
        IsLocked = p_lock;
        m_ColorChanger.Correct();

        m_SlotPairA.GetWordDrag().Lock(true);
        m_SlotPairB.GetWordDrag().Lock(true);
    }
}
