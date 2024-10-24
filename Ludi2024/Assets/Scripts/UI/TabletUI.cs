using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    private Camera m_Camera;
    private Animator m_Animator;

    private bool m_IsTabletEnabled;
    private void Awake()
    {
        m_Camera = GetComponentInParent<Camera>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_IsTabletEnabled = true;
        m_Animator.SetBool("Show", m_IsTabletEnabled);
    }

    private void Update()
    {
        if (InputManager.Instance.Esc.Tap)
        {
            OnEnableTablet(!m_IsTabletEnabled);
        }
    }

    private void OnEnable()
    {
        MainMenu.OnEnableTablet += OnEnableTablet;
        NotebookUI.OnEnableTablet += OnEnableTablet;
    }

    private void OnDisable()
    {
        MainMenu.OnEnableTablet -= OnEnableTablet;
        NotebookUI.OnEnableTablet -= OnEnableTablet;
    }

    public void OnEnableTablet(bool p_IsEnable)
    {
        m_IsTabletEnabled = p_IsEnable;
        GameManager.Instance.EnablePlayerMovement(!p_IsEnable);
        m_Animator.SetBool("Show", p_IsEnable);
    }

    public bool IsTabletEnabled()
    {
        return m_IsTabletEnabled;
    }
}
