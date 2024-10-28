using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Scenes m_NextScene;

    public void LoadNextLevel()
    {
        GameManager.Instance.m_IsWorldCompleted = false;
        GameManager.Instance.m_IsTutorialCompleted = false;
        GameManager.Instance.m_StartPosition = GameManager.Instance.m_OriginalPlayerPosition;
        GameManager.Instance.m_StartRotation = GameManager.Instance.m_OriginalPlayerRotation;
        GameManager.Instance.LoadScene(m_NextScene);
    }
}
