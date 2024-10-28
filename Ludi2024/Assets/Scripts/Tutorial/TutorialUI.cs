using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Canvas m_BlockingCanvas;

    [Header("Audio")] 
    [SerializeField] private EventReference m_NextPageSound;

    [Header("Settings")]
    [SerializeField] private bool m_IsNoteBookEnabled;
    
    private EventInstance m_NextPageSoundInstance;

    private void Start()
    {
        m_NextPageSoundInstance = RuntimeManager.CreateInstance(m_NextPageSound);
    }

    private void OnDisableTutorialNoteBook()
    {
        m_BlockingCanvas.enabled = false;
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
        m_NextPageSoundInstance.start();
    }

    private void OnDestroy()
    {
        m_NextPageSoundInstance.release();
    }
}
