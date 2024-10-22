using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotebookUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_InfoPanel;
    [SerializeField] private GameObject m_BulletPointHolder;
    [SerializeField] private GameObject m_BulletPointPrefab;
    [SerializeField] private Animator m_Animator;

    [Header("Settings")]
    [SerializeField] private int m_MaxBPForPage = 4;

    private List<BulletPoint> m_BulletPoints;
    private List<string> m_BulletPointTexts;

    private int m_CurrentIndex;
    private bool m_IsNoteBookEnabled;

    private void Start()
    {
        m_BulletPoints = new List<BulletPoint>();
        m_BulletPointTexts = new List<string>();

        for (int i = 0; i < m_MaxBPForPage; i++)
        {
            GameObject bulletPoint = Instantiate(m_BulletPointPrefab, m_BulletPointHolder.transform);
            m_BulletPoints.Add(bulletPoint.GetComponent<BulletPoint>());
        }

        m_CurrentIndex = 0;
        m_IsNoteBookEnabled = false;
        m_Animator.SetTrigger("Hide");
    }

    private void Update()
    {
        if (InputManager.Instance.Tab.Tap)
        {
            Debug.Log("Tab");
            m_IsNoteBookEnabled = !m_IsNoteBookEnabled;
            OnEnableNoteBook(m_IsNoteBookEnabled);
        }

        if(!m_IsNoteBookEnabled) return;

        if (InputManager.Instance.Q.Tap)
        {
            PreviousPage();
        }

        if (InputManager.Instance.E.Tap)
        {
            NextPage();
        }
    }

    private void OnEnable()
    {
        GameManager.OnEnableNoteBook += OnEnableNoteBook;
        GameManager.OnAddBulletPoint += AddBulletPoint;
    }

    private void OnDisable()
    {
        GameManager.OnEnableNoteBook -= OnEnableNoteBook;
        GameManager.OnAddBulletPoint -= AddBulletPoint;
    }

    private void SetNextIndex()
    {
        int l_index = m_CurrentIndex;

        if (l_index + m_MaxBPForPage > m_BulletPointTexts.Count - 1)
        {
            m_CurrentIndex = 0;
        }
        else
        {
            m_CurrentIndex += m_MaxBPForPage;
        }
    }

    private void SetPreviousPage()
    {
        int l_index = m_CurrentIndex;

        if (l_index + m_MaxBPForPage > m_BulletPointTexts.Count - 1)
        {
            m_CurrentIndex = 0;
        }
        else
        {
            m_CurrentIndex -= m_MaxBPForPage;
        }
    }

    private void ClearBulletPoints()
    {
        foreach (var bp in m_BulletPoints)
        {
            bp.ClearText();
        }
    }

    private void PassPage()
    {
        int l_index = m_CurrentIndex;

        for (int i = 0; i < m_MaxBPForPage; i++)
        {
            if (l_index < m_BulletPointTexts.Count)
            {
                m_BulletPoints[i].SetText(m_BulletPointTexts[l_index]);
                l_index++;
            }
            else
            {
                break;
            }
        }
    }

    public void OnEnableNoteBook(bool p_enable)
    {
        
        m_Animator.SetBool("Show", p_enable);
        m_InfoPanel.SetActive(!p_enable);
    }

    public void AddBulletPoint(string p_text)
    {
        m_BulletPointTexts.Add(p_text);

        foreach (var bp in m_BulletPoints.Where(bp => bp.IsEmpty()))
        {
            bp.SetText(p_text);
            break;
        }
    }

    public void NextPage()
    {
        if(m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        SetNextIndex();
        ClearBulletPoints();
        m_Animator.SetTrigger("NextPage");

        PassPage();
    }

    public void PreviousPage()
    {
        if (m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        SetPreviousPage();
        ClearBulletPoints();
        m_Animator.SetTrigger("PreviousPage");

        PassPage();
    }
}
