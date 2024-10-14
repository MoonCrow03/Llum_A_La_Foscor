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

    private bool m_Locked;
    private ConnectWordsSlot m_CurrentWordSlot;

    private void Start()
    {
        SetWordSlot(GetComponentInParent<ConnectWordsSlot>());

        m_TextMeshProUGUI.text = m_Word;
        m_Locked = false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (m_Locked) return;

        base.OnBeginDrag(eventData);
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

    public void LockWord()
    {
        m_Locked = true;
    }

    public void SetWordSlot(ConnectWordsSlot p_slot)
    {
        m_CurrentWordSlot = p_slot;
        m_CurrentWordSlot.SetWord(m_Word);
    }
    public ConnectWordsSlot GetWordSlot()
    {
        return m_CurrentWordSlot;
    }
}
