using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_Play;
    [SerializeField] private Button m_Quit;

    public void StartGame()
    {
        GameEvents.TriggerEnableTablet(false);
        GameEvents.TriggerEnablePlayerMovement(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(Scenes.World01.ToString());
    }
}
