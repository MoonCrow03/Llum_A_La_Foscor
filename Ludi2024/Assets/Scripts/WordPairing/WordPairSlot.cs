using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WordPairSlot : SlotContainer2D
{
    public static event Action OnWordDropped;

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        StartCoroutine(OnCheckPair());
    }

    private IEnumerator OnCheckPair()
    {
        yield return new WaitForSeconds(0.1f);
        OnWordDropped?.Invoke();
    }

    public bool HasWord()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();

        if(l_drag == null) return false;

        return l_drag.GetWordId() != -1;
    }

    public int GetWordId()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();

        Debug.Log(l_drag.GetWordPair() + " ID: " + l_drag.m_Id);

        if (l_drag == null) return -1;

        return l_drag.GetWordId();
    }

    public WordPairDrag GetWordDrag()
    {
        WordPairDrag l_drag = GetComponentInChildren<WordPairDrag>();
        return transform.GetComponentInChildren<WordPairDrag>();
    }
}
