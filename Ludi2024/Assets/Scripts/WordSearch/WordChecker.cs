using System;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using UnityEngine;
using Utilities;
using Debug = UnityEngine.Debug;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private BoardData m_BoardData;
    
    [Header("Audio")]
    public EventReference m_AudioEventWin;
    public EventReference m_AudioEventLose;
    
    [Header("Time")]
    [SerializeField] private float m_Time;
    [SerializeField] private TextMeshProUGUI m_ClockText;
    
    [Header("Points")]
    [SerializeField] private float m_PointsMultiplier = 1.0f;
    
    
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
    
    private TimeLimit m_TimeLimit;
    private bool m_IsGameCompleted = false;
    
    private FMOD.Studio.EventInstance m_AudioInstanceWin;
    private FMOD.Studio.EventInstance m_AudioInstanceLose;

    private void Start()
    {
        m_AssignedPoints = 0;
        m_CompletedWords = 0;
        m_CorrectSquareList = new List<int>();
        m_CurrentRay = new Ray();
        
        m_TimeLimit = new TimeLimit(this);
        m_TimeLimit.StartTimer(m_Time, LoseGame);

        m_AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(m_AudioEventWin);
        m_AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(m_AudioEventLose);
    }

    private void Update()
    {
        /*if (m_AssignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(m_RayUp.origin, m_RayUp.direction * 4);
            Debug.DrawRay(m_RayDown.origin, m_RayDown.direction * 4);
            Debug.DrawRay(m_RayLeft.origin, m_RayLeft.direction * 4);
            Debug.DrawRay(m_RayRight.origin, m_RayRight.direction * 4);
            Debug.DrawRay(m_RayDiagonalLeftUp.origin, m_RayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(m_RayDiagonalLeftDown.origin, m_RayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(m_RayDiagonalRightUp.origin, m_RayDiagonalRightUp.direction * 4);
            Debug.DrawRay(m_RayDiagonalRightDown.origin, m_RayDiagonalRightDown.direction * 4);
        }*/

        TimeSpan timeSpan = TimeSpan.FromSeconds(m_TimeLimit.GetTimeRemaining());
        m_ClockText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
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
            if (m_Word.Equals(t_searchingWord.m_Word) || m_Word.Equals(ReverseString(t_searchingWord.m_Word)))
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

    private string ReverseString(string p_word)
    {
        char[] l_charArray = p_word.ToCharArray();
        Array.Reverse(l_charArray);
        return new string(l_charArray);
    }

    private bool IsPointOnTheRay(Ray p_currentRay, Vector3 p_point)
    {
        // Convert Vector3 to Vector2 for 2D calculations
        Vector2 l_rayOrigin = new Vector2(p_currentRay.origin.x, p_currentRay.origin.y);
        Vector2 l_rayDirection = new Vector2(p_currentRay.direction.x, p_currentRay.direction.y).normalized;
        Vector2 l_point = new Vector2(p_point.x, p_point.y);

        // Get the vector from the ray's origin to the point
        Vector2 l_originToPoint = l_point - l_rayOrigin;

        // Project the vector onto the ray's direction
        float l_projectionLength = Vector2.Dot(l_originToPoint, l_rayDirection);

        // If projection length is negative, the point is behind the ray's origin
        if (l_projectionLength < 0)
        {
            return false;
        }

        // Get the closest point on the ray to the target point
        Vector2 l_closestPointOnRay = l_rayOrigin + l_rayDirection * l_projectionLength;

        // Calculate the distance between the point and the closest point on the ray
        float l_distanceToRay = Vector2.Distance(l_point, l_closestPointOnRay);

        // Adjust the tolerance value if necessary
        float l_tolerance = 0.1f;

        // Return true if the distance is within the tolerance
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
            m_IsGameCompleted = true;
            m_TimeLimit.StopTimer();
            m_AudioInstanceWin.start();
            
            GameManager.Instance.Points += m_TimeLimit.GetPoints(m_PointsMultiplier);
            GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
        }
    }
    
    private void LoseGame()
    {
        if (m_IsGameCompleted) return;
        m_TimeLimit.StopTimer();
        m_AudioInstanceLose.start();
        GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
    }
}
