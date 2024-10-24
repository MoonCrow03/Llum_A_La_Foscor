using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private static Dictionary<Scenes, bool> miniGamesCompleted = new Dictionary<Scenes, bool>();
    
    public static Dictionary<Scenes, bool> MiniGamesCompleted => miniGamesCompleted;

    private bool m_CanPlayerMove;

    //TODO: Crear enum para trackear que minijuegos de cada nivel se han completado


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
        m_CanPlayerMove = false;
    }

    private void Update()
    {

    }

    public void LoadScene(Scenes p_scene)
    {
        SceneManager.LoadSceneAsync(p_scene.ToString());
    }
    
    public void SetMiniGameCompleted(Scenes minigameName)
    {
        if (!miniGamesCompleted.TryAdd(minigameName, true))
        {
            miniGamesCompleted.Add(minigameName, true);
        }
    }
    
    public bool IsMiniGameCompleted(Scenes minigameName)
    {
        return miniGamesCompleted.ContainsKey(minigameName) && miniGamesCompleted[minigameName];
    }
}
