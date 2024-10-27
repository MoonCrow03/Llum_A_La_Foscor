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
    
    public bool m_IsWorld01Completed = false;
    
    public bool m_IsWorld02Completed = false;
    
    
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
        if (p_scene == Scenes.World01)
        {
            if (AreAllLevel1MiniGamesCompleted())
            {
                m_IsWorld01Completed = true;
                Debug.Log("All level 1 minigames completed");
            }
            
            
        }
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
    
    public bool AreAllLevel1MiniGamesCompleted()
    {
        return m_NotebookData.AreAllLevel1NotesCompleted();
    }
    
    public bool AreAllLevel2MiniGamesCompleted()
    {
        return m_NotebookData.AreAllLevel2NotesCompleted();
    }
    
}
