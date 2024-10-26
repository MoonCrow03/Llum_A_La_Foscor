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
    [SerializeField] private bool m_IsNoteBookEnabled;

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
        if(!m_IsNoteBookEnabled) return;

        m_Animator.SetTrigger("NextPage");
    }
}
