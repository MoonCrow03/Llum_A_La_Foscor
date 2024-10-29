using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAlwaysCam : MonoBehaviour
{
    //make the object always look at the camera
    [SerializeField] private Transform m_Camera;
    [SerializeField] private GameManager m_GameManager;


    private void Start()
    {
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (m_GameManager != null && !m_GameManager.m_IsWorldCompleted)
        {
            transform.LookAt(m_Camera);
        }
    }
}
