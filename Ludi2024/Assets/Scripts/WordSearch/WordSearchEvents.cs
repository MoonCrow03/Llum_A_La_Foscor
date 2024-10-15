using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSearchEvents
{
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSquareSelection;

    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSquareSelection != null)
        {
            OnEnableSquareSelection();
        }
    }

    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;

    public static void DisableSquareSelectionMethod() {
        if (OnDisableSquareSelection != null)
        {
            OnDisableSquareSelection();
        }
    }

    public delegate void SelectSquare(Vector3 p_position);
    public static event SelectSquare OnSelectSquare;

    public static void SelectSquareMethod(Vector3 p_position)
    {
        if (OnSelectSquare != null)
        {
            OnSelectSquare(p_position);
        }
    }

    public delegate void CheckSquare(string p_letter, Vector3 p_squarePosition, int p_squareIndex);
    public static event CheckSquare OnCheckSquare;

    public static void CheckSquareMethod(string p_letter, Vector3 p_squarePosition, int p_squareIndex)
    {
        if (OnCheckSquare != null)
        {
            OnCheckSquare(p_letter, p_squarePosition, p_squareIndex);
        }
    }

    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;

    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
        {
            OnClearSelection();
        }
    }
}
