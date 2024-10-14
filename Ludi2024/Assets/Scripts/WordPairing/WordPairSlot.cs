using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WordPairSlot : SlotContainer2D
{
    [Header("Components")]
    [SerializeField] private WordPairSlot m_SlotPair;
    [SerializeField] private ColorChanger m_ColorChanger;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        WordPairDrag l_draggableObject = eventData.pointerDrag.GetComponent<WordPairDrag>();

        if(l_draggableObject == null) return;

        if (!m_SlotPair.HasWord()) return;

        if (l_draggableObject.IsCorrect(m_SlotPair.GetWord()))
        {
            m_SlotPair.GetWordDrag().LockWord(true);
            l_draggableObject.LockWord(true);
            m_ColorChanger.Correct();

            // Move the word object to the correct slot pair
            l_draggableObject.transform.SetParent(transform);

            // Reset its position inside the slot
            l_draggableObject.transform.localPosition = Vector3.zero;
        }
    }

    public bool HasWord()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();
        string l_word = l_drag.GetWord();

        return l_word != null && !l_word.Equals("");
    }

    public string GetWord()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();
        return l_drag.GetWord();
    }

    public string GetWordPair()
    {
        return m_SlotPair.GetWord();
    }

    public WordPairDrag GetWordDrag()
    {
        return transform.GetComponentInChildren<WordPairDrag>();
    }
}
