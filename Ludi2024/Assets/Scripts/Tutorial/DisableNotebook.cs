using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableNotebook : MonoBehaviour
{
    [SerializeField] private GameObject m_Notebook;

    public void DisableNotebookGameObject()
    {
        m_Notebook.SetActive(false);
    }
}
