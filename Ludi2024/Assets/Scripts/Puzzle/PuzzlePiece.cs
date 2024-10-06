using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    [Header("Puzzle Settings")]
    [SerializeField] private float m_Snap;
    [SerializeField] private bool m_Looked;

    public Vector3 m_SolutionPosition;
    public Quaternion m_SolutionRotation;

    private void Awake()
    {
        m_SolutionPosition = transform.position;
        m_SolutionRotation = transform.rotation;
        m_Looked = false;
    }

    private void Update()
    {
        if(m_Looked) return;

        if (Vector3.Distance(transform.position, m_SolutionPosition) < m_Snap)
        {
            Debug.Log("Snap");
            transform.position = m_SolutionPosition;
            m_Looked = true;
        }
    }

    public bool IsSolution()
    {
        return !m_Looked && Vector3.Distance(transform.position, m_SolutionPosition) < m_Snap;
    }

    public Vector3 GetSolutionPosition()
    {
        m_Looked = true;
        return m_SolutionPosition;
    }

    public bool CanDrag()
    {
        return !m_Looked;
    }
}
