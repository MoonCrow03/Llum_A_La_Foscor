using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterPlaceHolder : SlotContainer2D
{
    private Transform originalParent;
    private int m_Index;

    private void Awake()
    {
        // Store the original parent of the SlotContainer2D at initialization
        originalParent = transform.parent;
        m_Index = transform.GetSiblingIndex();
    }

    private void OnTransformParentChanged()
    {
        // If the parent has changed, revert it back to the original parent
        if (transform.parent != originalParent)
        {
            Debug.LogWarning("Attempt to change SlotContainer2D's parent was blocked.");
            transform.parent = originalParent;
            transform.SetSiblingIndex(m_Index);
        }
    }
    
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }

    public void SetLetter(string p_letter)
    {
        TextMeshProUGUI l_text = GetComponentInChildren<TextMeshProUGUI>();
        l_text.text = p_letter;
    }
}
