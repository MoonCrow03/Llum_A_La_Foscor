using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] private GameObject m_ExclamationMark1;
    [SerializeField] private GameObject m_ExclamationMark2;
    
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
        GameEvents.OnEnableExMarkTablet1 += EnableExclamation1;
        GameEvents.OnEnableExMarkTablet2 += EnableExclamation2;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableTablet -= OnEnableTablet;
        GameEvents.OnEnableExMarkTablet1 -= EnableExclamation1;
        GameEvents.OnEnableExMarkTablet2 -= EnableExclamation2;
    }

    private void OnEnableTablet(bool p_IsEnable)
    {
        m_IsTabletEnabled = p_IsEnable;
        GameEvents.TriggerEnablePlayerMovement(!p_IsEnable);
        m_Animator.SetBool("Show", p_IsEnable);

        if (m_ExclamationMark1.activeSelf && p_IsEnable)
        {
            EnableExclamation1(false);
        }
    }

    public bool IsTabletEnabled()
    {
        return m_IsTabletEnabled;
    }
    
    private void EnableExclamation1(bool p_enable)
    {
        m_ExclamationMark1.SetActive(p_enable);
    }
    
    private void EnableExclamation2(bool p_enable)
    {
        m_ExclamationMark2.SetActive(p_enable);
    }
}
