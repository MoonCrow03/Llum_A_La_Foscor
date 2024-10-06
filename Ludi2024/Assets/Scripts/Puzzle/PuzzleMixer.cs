using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMixer : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnLocations;

    private List<Transform> m_PuzzlePieces = new List<Transform>();
    private List<Transform> m_SpawnPoints = new List<Transform>();

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
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
            int l_randomIndex = Random.Range(0, m_SpawnPoints.Count-1);
            l_piece.position = m_SpawnPoints[l_randomIndex].position;
            m_SpawnPoints.RemoveAt(l_randomIndex);
        }
    }
}
