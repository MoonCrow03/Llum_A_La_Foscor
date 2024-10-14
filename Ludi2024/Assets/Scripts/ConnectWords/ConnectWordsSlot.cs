using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConnectWordsSlot : PlaceableSlot
{
    [Header("Components")]
    [SerializeField] private ConnectWordsSlot m_SlotPair;
    [SerializeField] private ColorChanger m_ColorChanger;

    public string m_CurrentWord;

    private void Start()
    {
        ConnectWordsDrag l_drag = GetComponentInChildren<ConnectWordsDrag>();
        m_CurrentWord = l_drag.GetWord();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        ConnectWordsDrag l_draggableObject = eventData.pointerDrag.GetComponent<ConnectWordsDrag>();

        if(l_draggableObject == null) return;

        l_draggableObject.SetWordSlot(this);

        if (!m_SlotPair.HasWord()) return;

        if (l_draggableObject.IsCorrect(m_SlotPair.GetWord()))
        {
            l_draggableObject.LockWord();
            m_ColorChanger.Correct();
        }
    }

    public bool HasWord()
    {
        return !m_CurrentWord.Equals("") && m_CurrentWord != null;
    }

    public string GetWord()
    {
        return m_CurrentWord;
    }

    public void SetWord(string p_word)
    {
        m_CurrentWord = p_word;
    }
}
