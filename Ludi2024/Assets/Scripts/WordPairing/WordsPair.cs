using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsPair : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private WordPairDrag.WhichWordPair m_WhichWordPairToLock;
    
    [Header("Components")]
    [SerializeField] private WordPairDrag m_WordAComponent;
    [SerializeField] private WordPairDrag m_WordBComponent;
    [SerializeField] private WordPairSlot m_SlotPairA;
    [SerializeField] private WordPairSlot m_SlotPairB;
    [SerializeField] private GameObject m_PointA;
    [SerializeField] private GameObject m_PointB;

    public bool IsLocked;

    private ColorChanger m_ColorChanger;

    private void Awake()
    {
        m_ColorChanger = GetComponent<ColorChanger>();
    }

    private void Start()
    {
        if (m_WhichWordPairToLock == WordPairDrag.WhichWordPair.A)
        {
            LockA();
        }
        else
        {
            LockB();
        }
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

    public bool IsPair()
    {
        return m_SlotPairA.GetWordId().Equals(m_SlotPairB.GetWordId());
    }

    public void LockWords(bool p_lock)
    {
        IsLocked = p_lock;
        m_ColorChanger.Correct();
        
        m_SlotPairB.GetWordDrag().Lock(true);
    }

    private void LockA()
    {
        m_WordAComponent.Lock(true);
        m_ColorChanger.ChangePointAColor();
    }
    
    private void LockB()
    {
        m_WordBComponent.Lock(true);
        m_ColorChanger.ChangePointBColor();
    }
}
