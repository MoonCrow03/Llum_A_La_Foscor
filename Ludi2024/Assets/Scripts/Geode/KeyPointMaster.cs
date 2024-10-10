using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPointMaster : MonoBehaviour
{
    [Header("Key Points Settings")]
    [SerializeField] private ParticleSystem m_Particles;
    [SerializeField] private float m_ParticleDuration;

    private GameObject m_SelectedObject;
    private Rotate m_Rotate;

    private void Awake()
    {
        m_Rotate = GetComponent<Rotate>();
    }

    private void Start()
    {
        m_Particles.Stop();
    }

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

                    m_Particles.transform.position = m_SelectedObject.transform.position;
                    m_Particles.Play();

                    StartCoroutine(DestroyObject());
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

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(m_ParticleDuration);

        Destroy(m_SelectedObject);
        m_SelectedObject = null;
    }
}
