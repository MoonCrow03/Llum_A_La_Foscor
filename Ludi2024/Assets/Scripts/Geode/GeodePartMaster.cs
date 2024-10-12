using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeodePartMaster : MonoBehaviour
{
    private GameObject m_SelectedObject;

    private void Update()
    {
        if (InputManager.Instance.LeftClick.Tap)
        {
            if (m_SelectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("KeyPoint")) return;

                    m_SelectedObject = hit.collider.gameObject;

                    GeodePart l_GeodePart = m_SelectedObject.GetComponent<GeodePart>();
                    l_GeodePart.OnKeyPointClicked();
                    m_SelectedObject = null;
                }
            }
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 l_screenMousePosFar = new Vector3(InputManager.Instance.MousePosition.x, InputManager.Instance.MousePosition.y, Camera.main.farClipPlane);
        Vector3 l_screenMousePosNear = new Vector3(InputManager.Instance.MousePosition.x, InputManager.Instance.MousePosition.y, Camera.main.nearClipPlane);

        Vector3 l_worldMousePosFar = Camera.main.ScreenToWorldPoint(l_screenMousePosFar);
        Vector3 l_worldMousePosNear = Camera.main.ScreenToWorldPoint(l_screenMousePosNear);

        RaycastHit l_hit;
        Physics.Raycast(l_worldMousePosNear, l_worldMousePosFar - l_worldMousePosNear, out l_hit);

        return l_hit;
    }
}
