using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Drop : MonoBehaviour
{
    private GameObject _selectedObject;

    private void Update()
    {
        // Pick up object
        if (InputManager.Instance.LeftClick.Hold)
        {
            if (_selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if(!hit.collider.CompareTag("Drag")) return;

                    _selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y,
                    Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                _selectedObject.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

                _selectedObject = null;
                Cursor.visible = true;
            }
        }

        // Drag object
        if (_selectedObject != null)
        {
            Vector3 position = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y,
                Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            _selectedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);

            // Rotate object
            if (InputManager.Instance.RightClick.Tap)
            {
                _selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    _selectedObject.transform.rotation.eulerAngles.x, 
                    _selectedObject.transform.rotation.eulerAngles.y + 90f, 
                    _selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear= new Vector3(InputManager.Instance.MouseInput.x, InputManager.Instance.MouseInput.y, Camera.main.farClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

}
