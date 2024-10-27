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

    public void OnShow(bool p_enable)
    {
        gameObject.SetActive(p_enable);
    }

    public void StartGame()
    {
        GameEvents.TriggerEnableTablet(false);
        GameEvents.TriggerEnablePlayerMovement(true);
        
        if(!GameManager.Instance.IsTutorialCompleted())
        {
            GameManager.Instance.EnableTutorialWorld();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
