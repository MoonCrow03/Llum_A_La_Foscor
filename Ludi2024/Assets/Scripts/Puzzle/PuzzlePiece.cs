using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private PuzzlePieceType m_PieceType;
    [SerializeField] private float m_SnapPosition = 0.5f;
    [SerializeField] private float m_SnapRotation = 1f;
    [SerializeField] private bool m_Looked;

    public Vector3 m_SolutionPosition;
    public Quaternion m_SolutionRotation;

    private void Awake()
    {
        if (m_PieceType == PuzzlePieceType.Simple)
        {
            m_SolutionPosition = transform.position;
            m_SolutionRotation = transform.rotation;
        }

        m_Looked = m_PieceType == PuzzlePieceType.First;
    }

    public void Snap()
    {
        // Check if the piece is already in the correct position
        if (m_Looked) return;

        // Check if the piece is in the correct rotation
        if (!(Quaternion.Angle(transform.rotation, m_SolutionRotation) < m_SnapRotation)) return;

        // Check if the piece is in the correct position
        if (!(Vector3.Distance(transform.position, m_SolutionPosition) < m_SnapPosition)) return;

        m_Looked = true;
        transform.position = m_SolutionPosition;
    }

    public bool CanDrag()
    {
        return !m_Looked;
    }

    public bool IsFirst()
    {
        return m_PieceType == PuzzlePieceType.First;
    }

}

public enum PuzzlePieceType
{
    Simple,
    Fake,
    First
}