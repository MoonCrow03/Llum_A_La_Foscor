using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorChanger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image m_PointA;
    [SerializeField] private Image m_PointB;
    [SerializeField] private Image m_Line;

    [Header("Color Settings")]
    [SerializeField] private Color m_CorrectColor;
    [SerializeField] private Color m_WrongColor;

    private void Awake()
    {
        ChangeColor(m_WrongColor);
    }

    public void Correct()
    {
        ChangeColor(m_CorrectColor);
    }

    public void Wrong()
    {
        ChangeColor(m_WrongColor);
    }

    private void ChangeColor(Color p_color)
    {
        m_Line.color = p_color;
        m_PointA.color = p_color;
        m_PointB.color = p_color;
    }
    
    public void ChangePointAColor()
    {
        m_PointA.color = m_CorrectColor;
    }
    
    public void ChangePointBColor()
    {
        m_PointB.color = m_CorrectColor;
    }
}
