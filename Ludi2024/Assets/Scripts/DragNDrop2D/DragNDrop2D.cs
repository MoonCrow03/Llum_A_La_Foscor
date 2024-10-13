using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class DragNDrop2D : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform m_ParentAfterDrag;

    private Image m_Image;
    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
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

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = InputManager.Instance.MousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(m_ParentAfterDrag);
        m_Image.raycastTarget = true;

        if (m_Text != null)
        {
            m_Text.raycastTarget = true;
        }
    }

    public void SetParentAfterDrag(Transform p_transform)
    {
        m_ParentAfterDrag = p_transform;
    }
}
