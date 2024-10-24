using System.Collections;
using System.Collections.Generic;
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

    public delegate void ShowEndgameMessage(string message, bool p_won);
    public static event ShowEndgameMessage OnSetEndgameMessage;

    public static void TriggerSetEndgameMessage(string p_message, bool p_won)
    {
        if (OnSetEndgameMessage != null)
        {
            OnSetEndgameMessage(p_message, p_won);
        }
    }

    public delegate void AddBulletPoint(string p_text);
    public static event AddBulletPoint OnAddBulletPoint;

    public static void TriggerAddBulletPoint(string p_text)
    {
        if (OnAddBulletPoint != null)
        {
            OnAddBulletPoint(p_text);
        }
    }

    public delegate void EnablePlayerMovement(bool p_enable);
    public static event EnablePlayerMovement OnEnablePlayerMovement;

    public static void TriggerEnablePlayerMovement(bool p_enable)
    {
        if (OnEnablePlayerMovement != null)
        {
            OnEnablePlayerMovement(p_enable);
        }
    }
}
