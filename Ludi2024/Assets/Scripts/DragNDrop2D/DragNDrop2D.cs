using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class DragNDrop2D : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform m_ParentAfterDrag;
    private SlotContainer2D mCurrentSlotContainer2D;

    private Image m_Image;
    private TextMeshProUGUI m_Text;

    private bool m_IsLocked;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
        mCurrentSlotContainer2D = GetComponentInParent<SlotContainer2D>();
    }
    private void Start()
    {
        m_IsLocked = false;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if(m_IsLocked) return;

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

    public void SetCurrentSlot(SlotContainer2D slotContainer2D)
    {
        mCurrentSlotContainer2D = slotContainer2D;
    }

    public SlotContainer2D GetCurrentSlot()
    {
        return mCurrentSlotContainer2D;
    }

    public void Lock(bool p_lock)
    {
        m_IsLocked = p_lock;
    }
}
