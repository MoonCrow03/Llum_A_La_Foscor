using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class GeodePartMaster : MonoBehaviour
{
    private GameObject m_SelectedObject;
    private bool m_GameStarted = false;

    [SerializeField] private bool m_IsTutorial;

    private void Start()
    {
        if (!m_IsTutorial || GameManager.TutorialsShown.ContainsKey(Scenes.GeodeLvl01))
        {
            m_GameStarted = true;
        }
    }

private void Update()
    {
        //TODO: Arreglar esto
        if (!m_GameStarted) return;

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

    private void StartGame()
    {
        m_GameStarted = true;
    }

    private void OnEnable()
    {
        TutorialText.OnTutorialFinished += StartGame;
    }

    private void OnDisable()
    {
        TutorialText.OnTutorialFinished -= StartGame;
    }
}
