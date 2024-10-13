using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSetter : MonoBehaviour
{
    [SerializeField] private Transform[] m_Points;
    [SerializeField] private Transform m_LineParent;
    [SerializeField] private GameObject m_LinePrefab;

    private void Start()
    {
        Instantiate(m_LinePrefab, m_LineParent);
    }
}
