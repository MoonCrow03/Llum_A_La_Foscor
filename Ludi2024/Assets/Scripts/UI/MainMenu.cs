using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_NextLevelMessage;
    [SerializeField] private GameObject m_PopUp;
    [SerializeField] private GameObject m_Options;
    
    [Header("Audio")]
    [SerializeField] private EventReference m_UIErrorSound;
    [SerializeField] private EventReference m_UISuccesSound;
    
    private EventInstance m_UIErrorSoundInstance;
    private EventInstance m_UISuccesSoundInstance;

    private void Start()
    {
        m_UIErrorSoundInstance = RuntimeManager.CreateInstance(m_UIErrorSound);
        m_UISuccesSoundInstance = RuntimeManager.CreateInstance(m_UISuccesSound);
    }

    public void StartGame()
    {
        OnShowPopUp(false);
        m_NextLevelMessage.SetActive(false);
        GameEvents.TriggerEnableTablet(false);
        GameEvents.TriggerEnablePlayerMovement(true);
    }

    public void QuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetMiniGames();
            Destroy(GameManager.Instance.gameObject);
        }
        
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(Scenes.World01.ToString());
    }

    public void LoadNextLevel()
    {
        if (GameManager.Instance.m_IsWorldCompleted)
        {
            m_UISuccesSoundInstance.start();
            m_Options.SetActive(false);
            m_PopUp.SetActive(false);
            m_NextLevelMessage.SetActive(true);
            GameEvents.TriggerEnableExMarkTablet2(false);
        }
        else
        {
            m_UIErrorSoundInstance.start();
            OnShowPopUp(true);
        }
    }

    public void OnShowPopUp(bool p_enable)
    {
        m_PopUp.SetActive(p_enable);
        m_Options.SetActive(!p_enable);
    }

    public void LoadMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetMiniGames();
            Destroy(GameManager.Instance.gameObject);
        }
        
        SceneManager.LoadSceneAsync(Scenes.MainMenu.ToString());
    }

    public void LoadCredits()
    {
        SceneManager.LoadSceneAsync(Scenes.End.ToString());
    }

    private void OnDestroy()
    {
        m_UIErrorSoundInstance.release();
        m_UISuccesSoundInstance.release();
    }
}
