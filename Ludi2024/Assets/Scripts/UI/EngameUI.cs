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

    private Animation m_Animation;
    private bool m_GameWon;

    private void Start()
    {
        m_Animation = GetComponent<Animation>();
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

    private void SetMessage(string p_message, bool p_won, int p_stars)
    {
        m_Message.text = p_message;
        m_GameWon = p_won;

        if (p_won)
        {
            GameManager.Instance.SetMiniGameCompleted(m_MiniGame);
        }
        
        GameEvents.TriggerMarkTutorialAsSeen(m_MiniGame);

        EnableEndgamePanel(true);

        if (m_Animation != null)
        {
            StartCoroutine(WaitForTabletToAppear(p_stars));
        }
        else
        {
            GameEvents.TriggerShowStars(p_stars);
        }
    }

    public bool WasGameWon()
    {
        return m_GameWon;
    }

    public void LoadWorldScene()
    {
        GameManager.Instance.LoadScene(m_World);
    }

    private IEnumerator WaitForTabletToAppear(int p_stars)
    {
        m_Animation.Play();
        yield return new WaitForSeconds(m_Animation.clip.length);
        GameEvents.TriggerShowStars(p_stars);
    }
}
