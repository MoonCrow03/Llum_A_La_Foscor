using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordPairDrag : DragNDrop2D
{
    private enum WhichWordPair
    {
        A,
        B
    }

    [Header("Word Settings")]
    [SerializeField] private WhichWordPair m_WhichWordPair;
    [SerializeField] private string m_WordShown;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private (string,string) m_WordPair;
    public int m_Id;

    private void Start()
    {
        m_WordShown = m_WhichWordPair == WhichWordPair.A ?
            m_WordPair.Item1 : m_WordPair.Item2;

        m_TextMeshProUGUI.text = m_WordShown;
    }

    public bool IsCorrect((string p_wordA, string p_wordB) p_wordPair)
    {
        return p_wordPair.Equals(m_WordPair);
    }

    public void SetWords((string p_wordA, string p_wordB) p_wordPair, int p_id)
    {
        m_Id = p_id;
        m_WordPair = p_wordPair;

        m_WordShown = m_WhichWordPair == WhichWordPair.A ? 
            m_WordPair.Item1 : m_WordPair.Item2;

        m_TextMeshProUGUI.text = m_WordShown;
    }

    public int GetWordId()
    {
        return m_Id;
    }

    public (string, string) GetWordPair()
    {
        return m_WordPair;
    }

    public override void Lock(bool p_lock)
    {
        Debug.Log(m_WordPair + " is locked");
        base.Lock(p_lock);
    }

    public WordPairSlot GetWordSlot()
    {
        WordPairSlot l_wordPairSlot = transform.GetComponentInParent<WordPairSlot>();
        return l_wordPairSlot;
    }
}
