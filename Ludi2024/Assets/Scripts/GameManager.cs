using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private NotebookData m_NotebookData;
    

    private static Dictionary<Scenes, bool> miniGamesCompleted = new Dictionary<Scenes, bool>();
    
    public static Dictionary<Scenes, bool> MiniGamesCompleted => miniGamesCompleted;

    private int points;
    
    public bool m_IsWorldCompleted = false;
    
    
    public bool m_IsTutorialCompleted;
    
    
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
    
    private void Start()
    {
        EnableTutorialWorld();
        
        
        if(m_IsWorldCompleted)
        {
            GameEvents.TriggerLevelComplete();
            GameEvents.TriggerEnablePlayerMovement(false);
        }
    }

    public List<NotebookData.Note> GetNotebookData01()
    {
        return new List<NotebookData.Note>(m_NotebookData.Notes.Where(note => note.Key.ToString().Contains("Lvl01")));
    }
    
    public List<NotebookData.Note> GetNotebookData02()
    {
        return new List<NotebookData.Note>(m_NotebookData.Notes.Where(note => note.Key.ToString().Contains("Lvl02")));
    }

    public void LoadScene(Scenes p_scene)
    {
        if (p_scene == Scenes.World01)
        {
            if (AreAllLevel1MiniGamesCompleted())
            {
                m_IsWorldCompleted = true;
                Debug.Log("All level 1 minigames completed");
            }
        }

        if (p_scene == Scenes.World02)
        {
            if (AreAllLevel2MiniGamesCompleted())
            {
                m_IsWorldCompleted = true;
                Debug.Log("All level 2 minigames completed");
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

    private void EnableTutorialWorld()
    {
        if(m_IsTutorialCompleted) return;
        
        m_IsTutorialCompleted = true;
        GameEvents.TriggerEnableTutorialWorldUI(true);
        GameEvents.TriggerEnablePlayerMovement(false);
    }
}
