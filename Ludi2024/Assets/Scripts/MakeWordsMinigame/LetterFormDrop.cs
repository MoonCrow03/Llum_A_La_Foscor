using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterFormDrop : SlotContainer2D
{
    public static event Action OnLetterDropped;
    
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        Debug.Log("LetterFormDrop: OnDrop");
        StartCoroutine(InvokeOnLetterDropped());
    }
    
    private IEnumerator InvokeOnLetterDropped()
    {
        yield return new WaitForSeconds(0.1f);
        OnLetterDropped?.Invoke();
        Debug.Log("LetterFormDrop: InvokeOnLetterDropped");
    }
}
