using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private Camera m_Camera;
    private NotebookUI m_Notebook;
    private TabletUI m_Tablet;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Notebook = GetComponentInChildren<NotebookUI>();
        m_Tablet = GetComponentInChildren<TabletUI>();
    }

    private void Update()
    {
        if (InputManager.Instance.LeftClick.Tap)
        {
            Ray ray = m_Camera.ScreenPointToRay(InputManager.Instance.MousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.CompareTag("Notebook"))
                {
                    bool l_enable = !m_Notebook.IsNoteBookEnabled();
                    m_Notebook.OnEnableNoteBook(l_enable);
                }

                if (hit.collider.transform.CompareTag("Tablet"))
                {
                    bool l_enable = !m_Tablet.IsTabletEnabled();
                    m_Tablet.OnEnableTablet(l_enable);
                }
            }
        }
    }
}
