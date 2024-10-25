using System;
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
    [SerializeField] private int m_NumBulletPoints = 7;

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

        for (int i = 0; i < m_NumBulletPoints; i++)
        {
            m_BulletPointTexts.Add(string.Empty);
        }

        m_CurrentIndex = 0;
        m_IsNoteBookEnabled = false;

        NotebookData l_notebookData = GameManager.Instance.GetNotebookData();

        foreach (var t_note in l_notebookData.Notes)
        {
            if (t_note.IsCompleted)
            {
                AddBulletPoint(t_note.Content);
            }
        }
    }

    private void Update()
    {
        if (InputManager.Instance.Tab.Tap)
        {
            OnEnableNoteBook(!m_IsNoteBookEnabled);
        }

        if(!m_IsNoteBookEnabled) return;

        if (InputManager.Instance.Q.Tap)
        {
            Debug.Log("Previous Page");
            PreviousPage();
        }

        if (InputManager.Instance.E.Tap)
        {
            Debug.Log("Next Page");
            NextPage();
        }
    }

    private void OnEnable()
    {
        GameEvents.OnEnableNotebook += OnEnableNoteBook;
    }

    private void OnDisable()
    {
        GameEvents.OnEnableNotebook -= OnEnableNoteBook;
    }

    private bool SetNextIndex()
    {
        int l_index = m_CurrentIndex;

        if (l_index + m_MaxBPForPage < m_BulletPointTexts.Count - 1)
        {
            m_CurrentIndex += m_MaxBPForPage;
            return true;
        }

        return false;
    }

    private bool SetPreviousIndex()
    {
        int l_index = m_CurrentIndex;

        if (l_index - m_MaxBPForPage >= 0)
        {
            m_CurrentIndex -= m_MaxBPForPage;
            return true;
        }
        return false;
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

    private void OnEnableNoteBook(bool p_enable)
    {
        m_IsNoteBookEnabled = p_enable;
        GameEvents.TriggerEnableTablet(false);
        m_Animator.SetBool("Show", p_enable);
        m_InfoPanel.SetActive(!p_enable);
    }

    public bool IsNoteBookEnabled()
    {
        return m_IsNoteBookEnabled;
    }

    private void AddBulletPoint(string p_text)
    {
        m_BulletPointTexts.Add(p_text);

        foreach (var bp in m_BulletPoints.Where(bp => bp.IsEmpty()))
        {
            bp.SetText(p_text);
            break;
        }
    }

    private void NextPage()
    {
        if(m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        if(!SetNextIndex()) return;

        ClearBulletPoints();
        m_Animator.SetTrigger("NextPage");

        PassPage();
    }

    private void PreviousPage()
    {
        if (m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        if (!SetPreviousIndex()) return;

        ClearBulletPoints();
        m_Animator.SetTrigger("PrevPage");

        PassPage();
    }
}
