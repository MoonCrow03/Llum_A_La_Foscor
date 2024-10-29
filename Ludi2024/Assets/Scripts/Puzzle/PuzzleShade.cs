using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleShade : MonoBehaviour
{
    
    [SerializeField] private Animator m_Animator;
    [SerializeField] private string m_AnimationName;
    
    private AnimationClip m_AnimationClip;

    private void Awake()
    {
        m_AnimationClip = m_Animator.runtimeAnimatorController.animationClips[0];
    }

    private void SetShade(int p_stars)
    {
        StartCoroutine(SetShadeRoutine(p_stars));
    }

    private IEnumerator SetShadeRoutine(int p_stars)
    {
        Debug.Log("SetShadeRoutine", gameObject);
        m_Animator.Play(m_AnimationName);
        
        yield return new WaitForSeconds(m_AnimationClip.length);
        
        GameEvents.TriggerSetEndgameMessage("Felicitats!", true, p_stars);
    }

    private void OnEnable()
    {
        GameEvents.OnActivatePuzzleShader += SetShade;
    }
    
    private void OnDisable()
    {
        GameEvents.OnActivatePuzzleShader -= SetShade;
    }
}
