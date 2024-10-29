using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordPairDrag : DragNDrop2D
{
    public enum WhichWordPair
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

        WordsPairManager.Instance.SetWordPairDrag(this);
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
}
