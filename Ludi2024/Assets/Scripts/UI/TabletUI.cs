using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private bool m_IsTabletEnabled;

    public static Action<bool> OnEnableNoteBook;

    private void Start()
    {
        m_IsTabletEnabled = false;
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

    public void OnEnableTablet(bool p_IsEnable)
    {
        OnEnableNoteBook?.Invoke(p_IsEnable);
        m_Animator.SetBool("Show", p_IsEnable);
    }
}
