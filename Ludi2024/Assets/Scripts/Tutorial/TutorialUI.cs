using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Animator m_Animator;

    [Header("Settings")]
    [SerializeField] private int m_MaxPages = 2;

    private int m_CurrentIndex;
    private bool m_IsNoteBookEnabled;

    private List<string> m_Pages;
    private List<AnimationClip> m_Clips;

    private void Start()
    {
        m_Clips = m_Animator.runtimeAnimatorController.animationClips.ToList();

        m_Pages = new List<string>();

        m_IsNoteBookEnabled = true;
        m_CurrentIndex = 0;
    }

    private void Update()
    {
        if (!m_IsNoteBookEnabled) return;

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

    private void NextPage()
    {
        if (!SetNextIndex()) return;

        ClearBulletPoints();
        StartCoroutine(HideText("NextPage"));

        PassPage();
    }

    private void PreviousPage()
    {
        if (!SetPreviousIndex()) return;

        ClearBulletPoints();
        StartCoroutine(HideText("PrevPage"));

        PassPage();
    }

    private bool SetNextIndex()
    {
        int l_index = m_CurrentIndex;

        if (l_index + m_MaxPages < m_Pages.Count - 1)
        {
            m_CurrentIndex++;
            return true;
        }

        return false;
    }

    private IEnumerator HideText(string p_trigger)
    {
        m_Animator.SetTrigger(p_trigger);
        m_Text.gameObject.SetActive(false);

        yield return new WaitForSeconds(m_Clips.Find(clip => clip.name.Equals("Armature|NextPage")).length);

        m_Text.gameObject.SetActive(true);
    }

    private bool SetPreviousIndex()
    {
        int l_index = m_CurrentIndex;

        if (l_index - m_MaxPages >= 0)
        {
            m_CurrentIndex--;
            return true;
        }
        return false;
    }

    private void ClearBulletPoints()
    {
        m_Text.text = "";
    }

    private void PassPage()
    {
        m_Text.text = m_Pages[m_CurrentIndex];
    }

    private void OnDisableTutorialNoteBook()
    {
        m_IsNoteBookEnabled = false;
        m_Animator.SetTrigger("Hide");
    }

    private void OnEnable()
    {
        TutorialText.OnPageFinished += PassPageAnimation;
        TutorialText.OnTutorialFinished += OnDisableTutorialNoteBook;
    }

    private void OnDisable()
    {
        TutorialText.OnPageFinished -= PassPageAnimation;
        TutorialText.OnTutorialFinished -= OnDisableTutorialNoteBook;
    }

    private void PassPageAnimation()
    {
        m_Animator.SetTrigger("NextPage");
    }
}
