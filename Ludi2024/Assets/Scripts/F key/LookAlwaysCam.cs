using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAlwaysCam : MonoBehaviour
{
    [SerializeField] private Transform m_Camera;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_SpriteRenderer.enabled = false;
    }
    
    private void Update()
    {
        transform.LookAt(m_Camera);
    }
}