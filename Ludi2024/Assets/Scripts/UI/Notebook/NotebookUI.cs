using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class NotebookUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_InfoPanelTop;
    [SerializeField] private GameObject m_InfoPanelBellow;
    [SerializeField] private GameObject m_BulletPointHolder;
    [SerializeField] private GameObject m_BulletPointPrefab;
    [SerializeField] private Animator m_Animator;

    [Header("Settings")]
    [SerializeField] private int m_MaxBPForPage = 2;

    private List<BulletPoint> m_BulletPoints;
    private List<string> m_BulletPointTexts;
    private List<AnimationClip> m_Clips;

    private int m_NumBulletPoints;
    private int m_CurrentIndex;
    private bool m_IsNoteBookEnabled;

    private string m_EmptyString = "??????????";

    private void Start()
    {
        m_BulletPoints = new List<BulletPoint>();
        m_BulletPointTexts = new List<string>();

        m_Clips = m_Animator.runtimeAnimatorController.animationClips.ToList();

        m_NumBulletPoints = GameManager.Instance.GetNotebookData().Notes.Count;

        for (int i = 0; i < m_NumBulletPoints; i++)
        {
            m_BulletPointTexts.Add(m_EmptyString);
        }

        m_CurrentIndex = 0;
        m_IsNoteBookEnabled = false;

        NotebookData l_notebookData = GameManager.Instance.GetNotebookData();

        foreach (var t_note in l_notebookData.Notes.Where(t_note => t_note.IsCompleted))
        {
            AddBulletPoint(t_note.Content);
        }

        for (int i = 0; i < m_MaxBPForPage; i++)
        {
            GameObject l_gameObject = Instantiate(m_BulletPointPrefab, m_BulletPointHolder.transform);
            BulletPoint l_bulletPoint = l_gameObject.GetComponent<BulletPoint>();
            l_bulletPoint.SetText(m_BulletPointTexts[i]);
            m_BulletPoints.Add(l_bulletPoint);
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
            PreviousPage();
        }

        if (InputManager.Instance.E.Tap)
        {
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
        m_InfoPanelTop.SetActive(!p_enable);
    }

    public bool IsNoteBookEnabled()
    {
        return m_IsNoteBookEnabled;
    }

    private void AddBulletPoint(string p_text)
    {
        int l_empty = m_BulletPointTexts.IndexOf(m_EmptyString);

        if (l_empty != -1)
        {
            m_BulletPointTexts[l_empty] = p_text;
        }
    }

    private void NextPage()
    {
        if(m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        if(!SetNextIndex()) return;

        ClearBulletPoints();
        StartCoroutine(HideText("NextPage"));

        PassPage();
    }

    private void PreviousPage()
    {
        if (m_BulletPointTexts.Count <= m_MaxBPForPage) return;

        if (!SetPreviousIndex()) return;

        ClearBulletPoints();
        StartCoroutine(HideText("PrevPage"));

        PassPage();
    }

    private IEnumerator HideText(string p_trigger)
    {
        m_Animator.SetTrigger(p_trigger);
        m_BulletPointHolder.SetActive(false);
        m_InfoPanelBellow.SetActive(false);

        yield return new WaitForSeconds(m_Clips.Find(clip => clip.name.Equals("Armature|NextPage")).length);

        m_BulletPointHolder.SetActive(true);
        m_InfoPanelBellow.SetActive(true);
    }
}
