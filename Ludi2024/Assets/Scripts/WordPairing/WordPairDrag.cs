using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordPairDrag : DragNDrop2D
{
    [Header("Word Settings")]
    [SerializeField] private string m_Word;
    [SerializeField] private string m_WordPair;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private void Start()
    {
        m_TextMeshProUGUI.text = m_Word;
    }

    public bool IsCorrect(string p_word)
    {
        return m_Word.Equals(p_word);
    }

    public void InitializeWords(string p_word, string p_wordPair)
    {
        m_Word = p_word;
        m_WordPair = p_wordPair;
        m_TextMeshProUGUI.text = m_Word;
    }

    public void SetWord(string p_word)
    {
        m_Word = p_word;
        m_TextMeshProUGUI.text = m_Word;
    }

    public string GetWord()
    {
        return m_Word;
    }

    public override void Lock(bool p_lock)
    {
        Debug.Log(m_Word + " is locked");
        base.Lock(p_lock);
    }

    public WordPairSlot GetWordSlot()
    {
        WordPairSlot l_wordPairSlot = transform.GetComponentInParent<WordPairSlot>();
        return l_wordPairSlot;
    }
}
