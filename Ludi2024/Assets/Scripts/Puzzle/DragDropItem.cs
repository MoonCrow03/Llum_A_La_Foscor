using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropItem : MonoBehaviour
{
    [Header("Y Axis Settings")]
    [SerializeField] private float m_YAxis = 0.25f;
    [SerializeField] private float m_DefaultY = 0f;

    private GameObject m_SelectedObject;
    private PuzzlePiece m_PuzzlePiece;

    private void Awake()
    {
        m_PuzzlePiece = GetComponent<PuzzlePiece>();
    }

    private void Update()
    {
        if (!m_PuzzlePiece.CanDrag()) return;

        // Pick up object
        if (m_PuzzlePiece.CanDrag() && InputManager.Instance.LeftClick.Tap)  // Detect when left click starts
        {
            bool a = m_PuzzlePiece.CanDrag();
            Debug.Log("Pick up");
            if (m_SelectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag")) return;
                    Debug.Log("Drag");
                    m_SelectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
        }

        // Drag object while holding
        if (m_PuzzlePiece.CanDrag() && InputManager.Instance.LeftClick.Hold && m_SelectedObject != null)
        {
            MouseToWorldObjectPosition(m_YAxis);
        }

        // Release the object
        if (m_PuzzlePiece.CanDrag() && InputManager.Instance.LeftClick.Release && m_SelectedObject != null)
        {
            Debug.Log("Releasing Object");
            
            MouseToWorldObjectPosition(m_DefaultY);

            m_SelectedObject = null;
            Cursor.visible = true;
        }

        // Rotate object
        if (m_PuzzlePiece.CanDrag() && m_SelectedObject != null && InputManager.Instance.RightClick.Tap)
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

    private void MouseToWorldObjectPosition(float y)
    {
        Vector3 l_Position = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y,
            Camera.main.WorldToScreenPoint(m_SelectedObject.transform.position).z);
        Vector3 l_WorldPosition = Camera.main.ScreenToWorldPoint(l_Position);

        m_SelectedObject.transform.position = new Vector3(l_WorldPosition.x, y, l_WorldPosition.z);
    }

}
