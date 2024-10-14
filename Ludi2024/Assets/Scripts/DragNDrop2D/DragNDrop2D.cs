using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class DragNDrop2D : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform m_ParentAfterDrag;
    private PlaceableSlot m_CurrentSlot;

    private Image m_Image;
    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
        m_CurrentSlot = GetComponentInParent<PlaceableSlot>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        m_ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        m_Image.raycastTarget = false;

        if (m_Text != null)
        {
            m_Text.raycastTarget = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = InputManager.Instance.MousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        if (m_ParentAfterDrag != null)
        {
            transform.SetParent(m_ParentAfterDrag);
            transform.localPosition = Vector3.zero;
        }
        
        m_Image.raycastTarget = true;

        if (m_Text != null)
        {
            m_Text.raycastTarget = true;
        }
    }

    public virtual void SetParentAfterDrag(Transform p_transform)
    {
        m_ParentAfterDrag = p_transform;
    }

    public void SetCurrentSlot(PlaceableSlot slot)
    {
        m_CurrentSlot = slot;
    }

    public PlaceableSlot GetCurrentSlot()
    {
        return m_CurrentSlot;
    }
}
