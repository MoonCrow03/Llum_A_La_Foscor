using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelCompleteUI;
    [SerializeField] private GameObject m_MainMenu;
    
    private Animator m_Animator;
    
    private bool m_IsTabletEnabled;
    
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_IsTabletEnabled = false;
        //m_Animator.SetBool("Show", m_IsTabletEnabled);
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
        GameEvents.OnEnableTablet += OnEnableTablet;
        GameEvents.OnLevelComplete += ShowLevelCompleteUI;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableTablet -= OnEnableTablet;
        GameEvents.OnLevelComplete -= ShowLevelCompleteUI;
    }
    
    private void ShowLevelCompleteUI()
    {
        m_MainMenu.gameObject.SetActive(false);
        m_LevelCompleteUI.gameObject.SetActive(true);
        GameEvents.TriggerEnableTablet(true);
        GameEvents.TriggerEnablePlayerMovement(false);
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
