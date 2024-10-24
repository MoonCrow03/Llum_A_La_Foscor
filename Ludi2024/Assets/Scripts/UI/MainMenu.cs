using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_Play;
    [SerializeField] private Button m_Quit;
    [SerializeField] private TabletUI m_TabletUI;

    public static Action<bool> OnEnableTablet;

    public void OnShow(bool p_enable)
    {
        gameObject.SetActive(p_enable);
    }

    public void StartGame()
    {
        OnEnableTablet?.Invoke(false);
        GameManager.Instance.EnablePlayerMovement(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
