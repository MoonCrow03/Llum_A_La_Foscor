using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropMaster : MonoBehaviour
{
    [Header("Y Axis Settings")]
    [SerializeField] private float m_YGrabbed = 1f;
    [SerializeField] private float m_YGrounded = 0f;
    [SerializeField] private float m_YAboveObject = 0.25f;

    private GameObject m_SelectedObject;

    private void Update()
    {
        // Pick up object
        if (InputManager.Instance.LeftClick.Tap)  // Detect when left click starts
        {
            Debug.Log("Pick up");
            if (m_SelectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag")) return;
                    Debug.Log("Drag");
                    m_SelectedObject = hit.collider.gameObject;

                    PuzzlePiece l_PuzzlePiece = m_SelectedObject.GetComponent<PuzzlePiece>();

                    if (!l_PuzzlePiece.CanDrag())
                    {
                        m_SelectedObject = null;
                    }
                    else
                    {
                        Cursor.visible = false;
                    }
                }
            }
        }

        if (m_SelectedObject == null) return;

        // Drag object while holding
        if (InputManager.Instance.LeftClick.Hold)
        {
            Drag();
        }

        // Release the object
        if (InputManager.Instance.LeftClick.Release)
        {
            Debug.Log("Releasing Object");

            Release();

            PuzzlePiece l_PuzzlePiece = m_SelectedObject.GetComponent<PuzzlePiece>();

            l_PuzzlePiece.SetPosition();

            m_SelectedObject = null;
            Cursor.visible = true;
        }

        // Rotate object
        if (InputManager.Instance.RightClick.Tap)
        {
            Debug.Log("Rotate");
            m_SelectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                m_SelectedObject.transform.rotation.eulerAngles.x,
                m_SelectedObject.transform.rotation.eulerAngles.y + 90f,
                m_SelectedObject.transform.rotation.eulerAngles.z));
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 l_screenMousePosFar = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.farClipPlane);
        Vector3 l_screenMousePosNear = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.nearClipPlane);

        Vector3 l_worldMousePosFar = Camera.main.ScreenToWorldPoint(l_screenMousePosFar);
        Vector3 l_worldMousePosNear = Camera.main.ScreenToWorldPoint(l_screenMousePosNear);

        RaycastHit l_hit;
        Physics.Raycast(l_worldMousePosNear, l_worldMousePosFar - l_worldMousePosNear, out l_hit);

        return l_hit;
    }

    private void Drag()
    {
        Vector3 l_worldPosition = MouseToWorldObjectPosition();

        m_SelectedObject.transform.position = new Vector3(l_worldPosition.x, m_YGrabbed, l_worldPosition.z);
    }

    private void Release()
    {
        Vector3 l_worldPosition = MouseToWorldObjectPosition();

        // Perform raycast downwards from the object's current position to detect if there is something below
        RaycastHit l_hit;
        Vector3 l_objectPosition = m_SelectedObject.transform.position;

        // Cast a ray downward to check if there's any object beneath the selected object
        if (Physics.Raycast(l_objectPosition, Vector3.down, out l_hit, Mathf.Infinity))
        {
            // If something is below, set the Y position to the default value
            m_SelectedObject.transform.position = new Vector3(l_worldPosition.x, m_YAboveObject, l_worldPosition.z);
        }
        else
        {
            // If nothing is below, allow the object to follow the mouse position using the specified Y value
            m_SelectedObject.transform.position = new Vector3(l_worldPosition.x, m_YGrounded, l_worldPosition.z);
        }
    }

    private Vector3 MouseToWorldObjectPosition()
    {
        Vector3 l_position = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y,
            Camera.main.WorldToScreenPoint(m_SelectedObject.transform.position).z);

        return Camera.main.ScreenToWorldPoint(l_position);
    }

}
