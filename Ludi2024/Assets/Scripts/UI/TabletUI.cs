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
        m_IsTabletEnabled = true;
        m_Animator.SetBool("Show", m_IsTabletEnabled);
    }

    private void Update()
    {
        if (InputManager.Instance.Esc.Tap)
        {
            m_IsTabletEnabled = !m_IsTabletEnabled;
            OnEnableTablet(m_IsTabletEnabled);
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

    private void OnEnableTablet(bool p_IsEnable)
    {
        GameManager.Instance.EnablePlayerMovement(!p_IsEnable);
        m_Animator.SetBool("Show", p_IsEnable);
    }
}
