using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TutorialWorldUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject m_TutorialUI;
    [SerializeField] private GameObject m_NotebookUI;
    [SerializeField] private Animator m_Animator;
    
    private List<AnimationClip> m_Clips;

    private void Awake()
    {
        m_Clips = m_Animator.runtimeAnimatorController.animationClips.ToList();
        m_TutorialUI.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnEnableTutorialWorldUI += EnableTutorialUI;
        GameEvents.OnPageFinished += PassPageAnimation;
    }
    
    private void OnDisable()
    {
        GameEvents.OnEnableTutorialWorldUI -= EnableTutorialUI;
        GameEvents.OnPageFinished -= PassPageAnimation;
    }

    private void EnableTutorialUI(bool p_enable)
    {
        m_TutorialUI.SetActive(p_enable);

        if (p_enable)
        {
            StartCoroutine(WaitToStart(p_enable));
        }
        else
        {
            m_Animator.SetBool("Show", p_enable);
        }
        
        m_NotebookUI.SetActive(!p_enable);
    }
    
    private void PassPageAnimation()
    {
        // TODO: Add pass page sound effect
        m_Animator.SetTrigger("NextPage");
    }

    private IEnumerator WaitToStart(bool p_enable)
    {
        m_Animator.SetBool("Show", p_enable);
        yield return new WaitForSeconds(m_Clips.Find(clip => clip.name.Equals("ShowNoteBook")).length);
        GameEvents.TriggerStartTutorialWorld();
    }
}
