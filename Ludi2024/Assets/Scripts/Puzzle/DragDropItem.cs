using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropItem : MonoBehaviour
{
    [Header("Y Axis Settings")]
    [SerializeField] private float _yAxis = 0.25f;
    [SerializeField] private float _defaultY = 0f;


    private GameObject _selectedObject;

    private void Update()
    {
        // Pick up object
        if (InputManager.Instance.LeftClick.Tap)  // Detect when left click starts
        {
            Debug.Log("LeftClick");
            if (_selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag")) return;
                    Debug.Log("Dragging");
                    _selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
        }

        // Drag object while holding
        if (InputManager.Instance.LeftClick.Hold && _selectedObject != null)
        {
            MouseToWorldObjectPosition(_yAxis);
        }

        // Release the object
        if (InputManager.Instance.LeftClick.Release && _selectedObject != null)
        {
            Debug.Log("Releasing Object");

            MouseToWorldObjectPosition(_defaultY);

            _selectedObject = null;
            Cursor.visible = true;
        }

        // Rotate object
        if (_selectedObject != null && InputManager.Instance.RightClick.Tap)
        {
            Debug.Log("RightClick");
            _selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                _selectedObject.transform.rotation.eulerAngles.x,
                _selectedObject.transform.rotation.eulerAngles.y + 90f,
                _selectedObject.transform.rotation.eulerAngles.z));
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);


        return hit;
    }

    private void MouseToWorldObjectPosition(float y)
    {
        Vector3 position = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y,
            Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        _selectedObject.transform.position = new Vector3(worldPosition.x, y, worldPosition.z);
    }

}
