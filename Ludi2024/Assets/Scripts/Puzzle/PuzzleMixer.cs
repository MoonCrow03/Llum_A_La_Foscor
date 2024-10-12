using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMixer : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnLocations;

    private List<Transform> m_PuzzlePieces;
    private List<Transform> m_SpawnPoints;

    private DragNDropMaster m_DragNDropMaster;
    private Vector3 m_RotationAngle;

    private void Awake()
    {
        m_DragNDropMaster = GetComponent<DragNDropMaster>();

        m_PuzzlePieces = new List<Transform>();
        m_SpawnPoints = new List<Transform>();
    }

    private void Start()
    {
        m_RotationAngle = m_DragNDropMaster.GetRotationAngle();
        
        // Get all puzzle pieces
        for (int i = 0; i < transform.childCount; i++)
        {
            PuzzlePiece l_piece = transform.GetChild(i).GetComponent<PuzzlePiece>();

            // Skip the first piece
            if (l_piece.IsFirst()) continue;

            m_PuzzlePieces.Add(transform.GetChild(i));
        }

        float l_grounded = m_DragNDropMaster.GetYGrounded();

        // Get all spawn points
        for (int i = 0; i < m_SpawnLocations.childCount; i++)
        {
            Transform l_spawnPoint = m_SpawnLocations.GetChild(i);
            l_spawnPoint.position = new Vector3(l_spawnPoint.position.x, l_grounded, l_spawnPoint.position.z);

            m_SpawnPoints.Add(m_SpawnLocations.GetChild(i));
        }

        MixPuzzle();
    }

    private void MixPuzzle()
    {
        foreach (var t_piece in m_PuzzlePieces)
        {
            t_piece.position = GetRandomPosition();
            t_piece.rotation = GetRandomRotation(t_piece);
        }
    }

    private Quaternion GetRandomRotation(Transform p_piece)
    {
        int l_randomRot = Random.Range(1, 4);

        return Quaternion.Euler(new Vector3(
            p_piece.rotation.eulerAngles.x + m_RotationAngle.x * l_randomRot,
            p_piece.rotation.eulerAngles.y + m_RotationAngle.y * l_randomRot,
            p_piece.rotation.eulerAngles.z + m_RotationAngle.z * l_randomRot));
    }

    private Vector3 GetRandomPosition()
    {
        int l_randomPosIndex = Random.Range(0, m_SpawnPoints.Count - 1);

        Vector3 l_position = m_SpawnPoints[l_randomPosIndex].position;

        m_SpawnPoints.RemoveAt(l_randomPosIndex);

        return l_position;
    }
}
