using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleShade : MonoBehaviour
{
    
    [SerializeField] private Animator m_Animator;
    
    public void SetShade()
    {
        m_Animator.Play("Puzzle1Shade");
    }
}
