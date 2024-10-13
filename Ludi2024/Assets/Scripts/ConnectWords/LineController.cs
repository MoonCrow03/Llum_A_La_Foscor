using UnityEngine;

public class LineController : MonoBehaviour
{
    private Transform[] m_Points;
    private LineRenderer m_LineRenderer;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < m_Points.Length; i++)
        {
            m_LineRenderer.SetPosition(i, m_Points[i].position);
        }
    }

    public void SetPoints(Transform[] p_Points)
    {
        m_Points = p_Points;
        m_LineRenderer.positionCount = m_Points.Length;
    }
}