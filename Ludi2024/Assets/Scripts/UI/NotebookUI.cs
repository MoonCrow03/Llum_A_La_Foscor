using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NotebookUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_BulletPointHolder;
    [SerializeField] private GameObject m_BulletPointPrefab;

    [Header("Settings")]
    [SerializeField] private int m_MaxBPForPage = 7;

    private Canvas m_NotebookUI;
    private List<BulletPoint> m_BulletPoints;
    private List<string> m_BulletPointTexts;
    private int m_CurrentIndex;

    private void Awake()
    {
        m_NotebookUI = GetComponent<Canvas>();
    }

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
    }

    private void Update()
    {
        
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

    public void OnEnableNoteBook(bool p_enable)
    {
        gameObject.SetActive(p_enable);
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
        SetNextIndex();

        int l_counter = m_CurrentIndex;

        for (int i = 0; i < m_MaxBPForPage; i++)
        {
            if (l_counter < m_BulletPoints.Count)
            {
                m_BulletPoints[l_counter].SetText(m_BulletPointTexts[l_counter]);
                l_counter++;
            }
        }
    }
}
