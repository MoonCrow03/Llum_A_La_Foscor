using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Scenes m_NextScene;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.m_IsWorld01Completed = false;
        GameManager.Instance.m_IsWorld02Completed = false;
        GameManager.Instance.m_IsTutorialCompleted = false;
        GameManager.Instance.LoadScene(m_NextScene);
    }
}
