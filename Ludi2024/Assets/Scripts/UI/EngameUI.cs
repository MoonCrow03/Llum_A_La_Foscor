using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EngameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_EndgamePanel;
    [SerializeField] private TextMeshProUGUI m_Message;
    [SerializeField] private Scenes m_MiniGame;
    [SerializeField] private Scenes m_World;

    private bool m_GameWon;

    private void Start()
    {
        EnableEndgamePanel(false);
    }

    private void OnEnable()
    {
        GameEvents.OnSetEndgameMessage += SetMessage;
    }

    private void OnDisable()
    {
        GameEvents.OnSetEndgameMessage -= SetMessage;
    }

    private void EnableEndgamePanel(bool isEnabled)
    {
        m_EndgamePanel.SetActive(isEnabled);
    }

    private void SetMessage(string p_message, bool p_won)
    {
        m_Message.text = p_message;
        m_GameWon = p_won;

        if (p_won)
        {
            GameManager.Instance.SetMiniGameCompleted(m_MiniGame);
        }

        EnableEndgamePanel(true);
    }

    public bool WasGameWon()
    {
        return m_GameWon;
    }

    public void LoadWorldScene()
    {
        GameManager.Instance.LoadScene(m_World);
    }
}
