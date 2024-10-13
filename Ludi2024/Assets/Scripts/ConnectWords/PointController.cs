using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [Header("Components")]
    private Transform[] m_Points;

    private LineController mLine;

    private void Awake()
    {
        mLine = GetComponentInChildren<LineController>();
    }

    private void Start()
    {
        mLine.SetPoints(m_Points);
    }
}