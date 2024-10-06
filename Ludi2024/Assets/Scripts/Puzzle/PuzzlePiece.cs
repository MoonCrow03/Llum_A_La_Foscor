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

    public void SetPosition()
    {
        if (!m_Looked && Vector3.Distance(transform.position, m_SolutionPosition) < m_Snap)
        {
            m_Looked = true;
            transform.position = m_SolutionPosition;
        }
    }

    public bool CanDrag()
    {
        return !m_Looked;
    }
}
