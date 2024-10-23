using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private static Dictionary<ELevelsCompleted, bool> miniGamesCompleted = new Dictionary<ELevelsCompleted, bool>();
    
    public static Dictionary<ELevelsCompleted, bool> MiniGamesCompleted => miniGamesCompleted;

    public static Action<bool> OnEnableNoteBook;
    public static Action<string> OnAddBulletPoint;

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

    public void EnablePlayerMovement(bool p_enable)
    {
        m_CanPlayerMove = p_enable;
    }

    public bool CanPlayerMove()
    {
        return m_CanPlayerMove;
    }
    
    public void SetMiniGameCompleted(ELevelsCompleted minigameName)
    {
        if (!miniGamesCompleted.TryAdd(minigameName, true))
        {
            miniGamesCompleted.Add(minigameName, true);
        }
    }
    
    public bool IsMiniGameCompleted(ELevelsCompleted minigameName)
    {
        return miniGamesCompleted.ContainsKey(minigameName) && miniGamesCompleted[minigameName];
    }
}
