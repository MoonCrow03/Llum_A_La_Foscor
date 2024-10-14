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

    public static Action OnCheckPairs;

    public bool HasWord()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();

        if(l_drag == null) return false;

        string l_word = l_drag.GetWord();

        return l_drag.GetWord() != null && !l_drag.GetWord().Equals("");
    }

    public string GetWord()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();
        return l_drag.GetWord();
    }

    public string GetWordPair()
    {
        return !m_SlotPair.HasWord() ? null : m_SlotPair.GetWord();
    }

    public WordPairDrag GetWordDrag()
    {
        return transform.GetComponentInChildren<WordPairDrag>();
    }
}
