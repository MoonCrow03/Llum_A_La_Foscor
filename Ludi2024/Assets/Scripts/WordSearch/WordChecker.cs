using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static WordSearchEvents;
using Debug = UnityEngine.Debug;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private BoardData m_BoardData;

    private string m_Word;
    private int m_AssignedPoints;
    private int m_CompletedWords;

    private Ray m_RayUp, m_RayDown;
    private Ray m_RayLeft, m_RayRight;
    private Ray m_RayDiagonalLeftUp, m_RayDiagonalLeftDown;
    private Ray m_RayDiagonalRightUp, m_RayDiagonalRightDown;

    private Ray m_CurrentRay;

    private Vector3 m_RayStartPosition;
    private List<int> m_CorrectSquareList;

    private void Start()
    {
        m_AssignedPoints = 0;
        m_CompletedWords = 0;
        m_CorrectSquareList = new List<int>();
        m_CurrentRay = new Ray();
    }

    private void Update()
    {
        if (m_AssignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(m_RayUp.origin, m_RayUp.direction * 4);
            Debug.DrawRay(m_RayDown.origin, m_RayDown.direction * 4);
            Debug.DrawRay(m_RayLeft.origin, m_RayLeft.direction * 4);
            Debug.DrawRay(m_RayRight.origin, m_RayRight.direction * 4);
            Debug.DrawRay(m_RayDiagonalLeftUp.origin, m_RayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(m_RayDiagonalLeftDown.origin, m_RayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(m_RayDiagonalRightUp.origin, m_RayDiagonalRightUp.direction * 4);
            Debug.DrawRay(m_RayDiagonalRightDown.origin, m_RayDiagonalRightDown.direction * 4);
        }
    }

    private void OnEnable()
    {
        WordSearchEvents.OnCheckSquare += SquareSelected;
        WordSearchEvents.OnClearSelection += ClearSelection;
    }

    private void OnDisable()
    {
        WordSearchEvents.OnCheckSquare -= SquareSelected;
        WordSearchEvents.OnClearSelection -= ClearSelection;
    }

    private void SquareSelected(string p_letter, Vector3 p_squarePosition, int p_squareIndex)
    {
        // If it is first square
        if (m_AssignedPoints == 0)
        {
            m_RayStartPosition = p_squarePosition;
            m_CorrectSquareList.Add(p_squareIndex);
            m_Word += p_letter;

            m_RayUp = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(0f, 1));
            m_RayDown = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(0f, -1));
            m_RayLeft = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(-1, 0f));
            m_RayRight = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(1, 0f));
            m_RayDiagonalLeftUp = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(-1, 1));
            m_RayDiagonalLeftDown = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(-1, -1));
            m_RayDiagonalRightUp = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(1, 1));
            m_RayDiagonalRightDown = new Ray(new Vector2(p_squarePosition.x, p_squarePosition.y), new Vector2(1, -1));
        }
        else if(m_AssignedPoints == 1)
        {
            // If it is the second square
            m_CorrectSquareList.Add(p_squareIndex);
            m_CurrentRay = SelectRay(m_RayStartPosition, p_squarePosition);
            WordSearchEvents.SelectSquareMethod(p_squarePosition);
            m_Word += p_letter;
            CheckWord();
        }
        else
        {
            if (IsPointOnTheRay(m_CurrentRay, p_squarePosition))
            {
                m_CorrectSquareList.Add(p_squareIndex);
                WordSearchEvents.SelectSquareMethod(p_squarePosition);
                m_Word += p_letter;
                CheckWord();
            }
        }

        m_AssignedPoints++;
    }

    private void CheckWord()
    {
        foreach (var t_searchingWord in m_BoardData.m_SearchWords)
        {
            if (m_Word.Equals(t_searchingWord.m_Word))
            {
                WordSearchEvents.CorrectWordMethod(m_Word, m_CorrectSquareList);
                m_Word = string.Empty;
                m_CorrectSquareList.Clear();
                m_CompletedWords++;
                CheckBoardCompleted();
                return;
            }
        }
    }

    private bool IsPointOnTheRay(Ray p_currentRay, Vector3 p_point)
    {
        // Get the vector from the ray's origin to the point
        Vector3 l_originToPoint = p_point - p_currentRay.origin;

        // Project the vector onto the ray's direction
        float l_projectionLength = Vector3.Dot(l_originToPoint, p_currentRay.direction.normalized);

        // Get the closest point on the ray to p_point
        Vector3 l_closestPointOnRay = p_currentRay.origin + p_currentRay.direction.normalized * l_projectionLength;

        // Calculate the distance between the point and the closest point on the ray
        float l_distanceToRay = Vector3.Distance(p_point, l_closestPointOnRay);

        // Define a tolerance for how close the point should be to the ray
        float l_tolerance = 0.1f; // Adjust this value depending on the accuracy you need

        // Return true if the distance is less than the tolerance
        return l_distanceToRay < l_tolerance;
    }

    private Ray SelectRay(Vector2 p_firstPosition, Vector2 p_secondPosition)
    {
        var l_direction = (p_secondPosition - p_firstPosition).normalized;
        float l_tolerance = 0.01f;

        if (Math.Abs(l_direction.x) < l_tolerance && Math.Abs(l_direction.y - 1f) < l_tolerance)
        {
            return m_RayUp;
        }

        if (Math.Abs(l_direction.x) < l_tolerance && Math.Abs(l_direction.y - (-1f)) < l_tolerance)
        {
            return m_RayDown;
        }

        if (Math.Abs(l_direction.x - (-1f)) < l_tolerance && Math.Abs(l_direction.y) < l_tolerance)
        {
            return m_RayLeft;
        }

        if (Math.Abs(l_direction.x - 1f) < l_tolerance && Math.Abs(l_direction.y) < l_tolerance)
        {
            return m_RayRight;
        }

        if (l_direction is { x: < 0.0f, y: > 0.0f })
        {
            return m_RayDiagonalLeftUp;
        }

        if (l_direction is { x: < 0.0f, y: < 0.0f })
        {
            return m_RayDiagonalLeftDown;
        }

        if (l_direction is { x: > 0.0f, y: > 0.0f })
        {
            return m_RayDiagonalRightUp;
        }

        if (l_direction is { x: > 0.0f, y: < 0.0f })
        {
            return m_RayDiagonalRightDown;
        }

        return m_RayDown;
    }

    private void ClearSelection()
    {
        m_AssignedPoints = 0;
        m_CorrectSquareList.Clear();
        m_Word = string.Empty;
    }

    private void CheckBoardCompleted()
    {
        if (m_BoardData.m_SearchWords.Count == m_CompletedWords)
        {
            GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
            //GameManager.Instance.SetMiniGameCompleted(); TODO: Setear la escena de completed aqui
            Debug.Log("Game Ended!");
        }
    }
}
