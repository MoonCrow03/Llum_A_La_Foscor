using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private Dictionary<string, bool> miniGamesCompleted = new Dictionary<string, bool>();
    
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
    }
    

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Esc.Tap)
        {
            QuitGame();
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SetMinigameCompleted(string minigameName)
    {
        if (!miniGamesCompleted.ContainsKey(minigameName))
        {
            miniGamesCompleted[minigameName] = true;
        }
        else
        {
            miniGamesCompleted.Add(minigameName, true);
        }
    }
    
    public bool IsMiniGameCompleted(string minigameName)
    {
        return miniGamesCompleted.ContainsKey(minigameName) && miniGamesCompleted[minigameName];
    }
    
    
}
