using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator m_NoteBookAnimator;

    public void HideNoteBook()
    {
        m_NoteBookAnimator.SetBool("Hide", true);
    }

    public void ShowNoteBook()
    {
        m_NoteBookAnimator.SetBool("Hide", false);
    }
}
