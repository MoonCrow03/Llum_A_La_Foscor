using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMixer : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnLocations;

    private List<Transform> m_PuzzlePieces = new List<Transform>();
    private List<Transform> m_SpawnPoints = new List<Transform>();

    private DragNDropMaster m_DragNDropMaster;
    private Vector3 m_RotationAngle;

    private void Awake()
    {
        m_DragNDropMaster = GetComponent<DragNDropMaster>();
    }

    private void Start()
    {
        m_RotationAngle = m_DragNDropMaster.GetRotationAngle();

        for (int i = 0; i < transform.childCount; i++)
        {
            m_PuzzlePieces.Add(transform.GetChild(i));
        }

        for (int i = 0; i < m_SpawnLocations.childCount; i++)
        {
            m_SpawnPoints.Add(m_SpawnLocations.GetChild(i));
        }

        MixPuzzle();
    }

    private void MixPuzzle()
    {
        foreach (var l_piece in m_PuzzlePieces)
        {
            l_piece.position = GetRandomPosition();
            l_piece.rotation = GetRandomRotation(l_piece);
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
