using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;

public abstract class DragNDrop2D : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Components")]
    [SerializeField] private Canvas m_Canvas;

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
        if(IsLocked()) return;

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
        if (IsLocked()) return;
        
        // Get the camera associated with the World Space canvas
        Camera l_worldCamera = m_Canvas.worldCamera;

        // Get the current mouse position in screen space
        Vector3 l_screenPosition = InputManager.Instance.MousePosition;

        // Get the distance between the canvas and the camera
        float l_canvasToCameraDistance = Vector3.Distance(l_worldCamera.transform.position, m_Canvas.transform.position);

        // Set the z-position of the screen position to the distance from the camera to the canvas
        l_screenPosition.z = l_canvasToCameraDistance;

        // Convert the screen position to a world position based on the canvas camera
        Vector3 l_worldPosition = l_worldCamera.ScreenToWorldPoint(l_screenPosition);

        // Get the RectTransform of the canvas and the object being dragged
        RectTransform l_canvasRect = m_Canvas.GetComponent<RectTransform>();
        RectTransform l_objectRect = GetComponent<RectTransform>();

        // Calculate the canvas bounds in world space
        Vector3[] l_canvasCorners = new Vector3[4];
        l_canvasRect.GetWorldCorners(l_canvasCorners);

        // Calculate the half-size of the object in world space to account for its dimensions
        Vector3 l_objectSize = l_objectRect.rect.size;
        Vector3 l_objectHalfSize = new Vector3(l_objectSize.x * l_objectRect.lossyScale.x / 2, l_objectSize.y * l_objectRect.lossyScale.y / 2, 0);

        // Clamp the world position, considering the object's size
        l_worldPosition.x = Mathf.Clamp(l_worldPosition.x, l_canvasCorners[0].x + l_objectHalfSize.x, l_canvasCorners[2].x - l_objectHalfSize.x);
        l_worldPosition.y = Mathf.Clamp(l_worldPosition.y, l_canvasCorners[0].y + l_objectHalfSize.y, l_canvasCorners[2].y - l_objectHalfSize.y);

        // Update the dragged object's world position
        transform.position = l_worldPosition;
    }


    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (IsLocked()) return;


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

    public virtual bool IsLocked()
    {
        return m_IsLocked;
    }

    public virtual void Lock(bool p_lock)
    {
        m_IsLocked = p_lock;
    }
    
    public virtual void SetCanvas()
    {
        m_Canvas = GetComponentInParent<Canvas>();
    }
}
