using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    private Animator m_Animator;
    
    private bool m_IsTabletEnabled;
    
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_IsTabletEnabled = false;
    }

    private void Update()
    {
        if (InputManager.Instance.Esc.Tap || InputManager.Instance.LeftShift.Tap)
        {
            OnEnableTablet(!m_IsTabletEnabled);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnEnableTablet += OnEnableTablet;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableTablet -= OnEnableTablet;
    }

    private void OnEnableTablet(bool p_IsEnable)
    {
        m_IsTabletEnabled = p_IsEnable;
        GameEvents.TriggerEnablePlayerMovement(!p_IsEnable);
        m_Animator.SetBool("Show", p_IsEnable);
    }

    public bool IsTabletEnabled()
    {
        return m_IsTabletEnabled;
    }
}
