using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectWordsDrag : DragNDrop2D
{
    [Header("Word Settings")]
    [SerializeField] private string m_Word;
    [SerializeField] private string m_WordPair;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    private ConnectWordsSlot mCurrentWordSlot;

    private void Start()
    {
        SetWordSlot(GetComponentInParent<ConnectWordsSlot>());

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

    public void LockWord(bool p_lock)
    {
        base.Lock(p_lock);
    }

    public void SetWordSlot(ConnectWordsSlot pSlot)
    {
        mCurrentWordSlot = pSlot;
        mCurrentWordSlot.SetWord(m_Word);
    }
    public ConnectWordsSlot GetWordSlot()
    {
        return mCurrentWordSlot;
    }
}
