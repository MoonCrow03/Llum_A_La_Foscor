using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    private Image m_Star;

    private void Awake()
    {
        m_Star = GetComponent<Image>();
        m_Star.transform.localPosition = Vector3.zero;
    }

    public Image GetImage()
    {
        return m_Star;
    }

    public void SetStarLocalScale(Vector3 p_pos)
    {
        m_Star.transform.localScale = p_pos;
    }
}
