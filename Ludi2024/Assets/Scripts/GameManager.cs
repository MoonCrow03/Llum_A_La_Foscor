using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private NotebookData m_NotebookData;

    private static Dictionary<Scenes, bool> miniGamesCompleted = new Dictionary<Scenes, bool>();
    
    public static Dictionary<Scenes, bool> MiniGamesCompleted => miniGamesCompleted;

    private int points;
    
    public int Points
    {
        get => points;
        set => points = value;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public ref NotebookData GetNotebookData()
    {
        return ref m_NotebookData;
    }

    public void LoadScene(Scenes p_scene)
    {
        SceneManager.LoadSceneAsync(p_scene.ToString());
    }
    
    public void SetMiniGameCompleted(Scenes p_scene)
    {
        m_NotebookData.SetNoteCompleted(p_scene);
    }
    
    public bool IsMiniGameCompleted(Scenes p_scene)
    {
        return m_NotebookData.IsNoteCompleted(p_scene);
    }
}
