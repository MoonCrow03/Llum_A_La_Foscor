using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEvents
{
    public delegate void EnableTablet(bool p_enable);
    public static event EnableTablet OnEnableTablet;
    
    public static void TriggerEnableTablet(bool p_enable)
    {
        if (OnEnableTablet != null)
        {
            OnEnableTablet(p_enable);
        }
    }

    public delegate void EnableNotebook(bool p_enable);
    public static event EnableNotebook OnEnableNotebook;

    public static void TriggerEnableNotebook(bool p_enable)
    {
        if (OnEnableNotebook != null)
        {
            OnEnableNotebook(p_enable);
        }
    }
    
    
    public delegate void ShowEndgameMessage(string message, bool p_won, int p_stars);
    public static event ShowEndgameMessage OnSetEndgameMessage;

    public static void TriggerSetEndgameMessage(string p_message, bool p_won, int p_stars)
    {
        if (OnSetEndgameMessage != null)
        {
            OnSetEndgameMessage(p_message, p_won, p_stars);
        }
    }

    public delegate void SetMiniGameCompleted(Scenes p_minigameName);
    public static event SetMiniGameCompleted OnSetMiniGameCompleted;

    public static void TriggerSetMiniGameCompleted(Scenes p_minigameName)
    {
        if (OnSetMiniGameCompleted != null)
        {
            OnSetMiniGameCompleted(p_minigameName);
        }
    }

    public delegate void EnablePlayerMovement(bool p_enable);
    public static event EnablePlayerMovement OnEnablePlayerMovement;
    public static Action<bool> OnEnablePlayerMovementAction;

    public static void TriggerEnablePlayerMovement(bool p_enable)
    {
        if (OnEnablePlayerMovement != null)
        {
            OnEnablePlayerMovement(p_enable);
            Debug.Log("<color=green>TriggerEnablePlayerMovement</color>");
            OnEnablePlayerMovementAction?.Invoke(p_enable);
        }
    }

    public delegate void ShowStars(int p_numberOfStars);
    public static event ShowStars OnShowStars;

    public static void TriggerShowStars(int p_numberOfStars)
    {
        if (OnShowStars != null)
        {
            OnShowStars(p_numberOfStars);
        }
    }
    
    public delegate void EnableTutorialWorldUI(bool p_enable);
    public static event EnableTutorialWorldUI OnEnableTutorialWorldUI;
    
    public static void TriggerEnableTutorialWorldUI(bool p_enable)
    {
        if (OnEnableTutorialWorldUI != null)
        {
            OnEnableTutorialWorldUI(p_enable);
        }
    }

    public delegate void StartTutorialWorld();
    public static event StartTutorialWorld OnStartTutorialWorld;
    
    public static void TriggerStartTutorialWorld()
    {
        if (OnStartTutorialWorld != null)
        {
            OnStartTutorialWorld();
        }
    }
    
    public delegate void PageFinished();
    public static event PageFinished OnPageFinished;
    
    public static void TriggerPageFinished()
    {
        if (OnPageFinished != null)
        {
            OnPageFinished();
        }
    }
    
    public delegate void LevelComplete();
    public static event LevelComplete OnLevelComplete;
    
    public static void TriggerLevelComplete()
    {
        if (OnLevelComplete != null)
        {
            OnLevelComplete();
        }
    }

    public delegate void MarkTutorialAsSeen(Scenes p_scene);
    public static event MarkTutorialAsSeen OnMarkTutorialAsSeen;
    
    public static void TriggerMarkTutorialAsSeen(Scenes p_scene)
    {
        if (OnMarkTutorialAsSeen != null) OnMarkTutorialAsSeen(p_scene);
    }
    
    public delegate void SetPlayerPosition();
    public static event SetPlayerPosition OnSetPlayerPosition;
    
    public static void TriggerSetPlayerPosition()
    {
        if (OnSetPlayerPosition != null)
        {
            OnSetPlayerPosition();
        }
    }

    public delegate void ActivatePuzzleShader(int p_stars);
    public static event ActivatePuzzleShader OnActivatePuzzleShader;
    
    public static void TriggerActivatePuzzleShader(int p_stars)
    {
        if (OnActivatePuzzleShader != null)
        {
            OnActivatePuzzleShader(p_stars);
        }
    }
    
    public delegate void EnableExclamationMark(bool p_enable);
    public static event EnableExclamationMark OnEnableExclamationMark;
    
    public static void TriggerEnableExclamationMark(bool p_enable)
    {
        if (OnEnableExclamationMark != null)
        {
            OnEnableExclamationMark(p_enable);
        }
    }
    
    public delegate void EnableExMarkTablet1(bool p_enable);
    public static event EnableExMarkTablet1 OnEnableExMarkTablet1;
    
    public static void TriggerEnableExMarkTablet1(bool p_enable)
    {
        if (OnEnableExMarkTablet1 != null)
        {
            OnEnableExMarkTablet1(p_enable);
        }
    }
    
    public delegate void EnableExMarkTablet2(bool p_enable);
    public static event EnableExMarkTablet2 OnEnableExMarkTablet2;
    
    public static void TriggerEnableExMarkTablet2(bool p_enable)
    {
        if (OnEnableExMarkTablet2 != null)
        {
            OnEnableExMarkTablet2(p_enable);
        }
    }
}
