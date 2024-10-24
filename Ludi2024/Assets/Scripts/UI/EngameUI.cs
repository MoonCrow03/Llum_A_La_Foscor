using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EngameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Message;
    [SerializeField] private ELevelsCompleted m_MiniGame;

    private bool m_GameWon;

    // Delegate for setting the message
    public delegate void EndgameMessageDelegate(string message, bool p_won);
    public static event EndgameMessageDelegate OnSetEndgameMessage;

    // Delegate for enabling/disabling the panel
    public delegate void EndgamePanelDelegate(bool isEnabled);
    public static event EndgamePanelDelegate OnEnableEndgamePanel;

    // Static methods to trigger events
    public static void TriggerSetEndgameMessage(string p_message, bool p_won)
    {
        if (OnSetEndgameMessage != null)
        {
            OnSetEndgameMessage(p_message, p_won);
        }
    }

    public static void TriggerEnableEndgamePanel(bool p_isEnabled)
    {
        if (OnEnableEndgamePanel != null)
        {
            OnEnableEndgamePanel(p_isEnabled);
        }
    }

    private void Start()
    {
        EnableEndgamePanel(false);
    }

    private void OnEnable()
    {
        // Subscribe to events
        OnSetEndgameMessage += SetMessage;
        OnEnableEndgamePanel += EnableEndgamePanel;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        OnSetEndgameMessage -= SetMessage;
        OnEnableEndgamePanel -= EnableEndgamePanel;
    }

    private void SetMessage(string p_message, bool p_won)
    {
        m_Message.text = p_message;
        m_GameWon = p_won;

        if (p_won)
        {
            GameManager.Instance.SetMiniGameCompleted(m_MiniGame);
        }
    }

    private void EnableEndgamePanel(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("WorldScene 1");
    }
}
