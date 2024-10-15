using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsGrid : MonoBehaviour
{
    [SerializeField] private BoardData m_CurrentBoard;
    [SerializeField] private GameObject m_GridSquarePrefab;
    [SerializeField] private AlphabetData m_AlphabetData;

    [SerializeField] private float m_SquareOffset = 0.0f;
    [SerializeField] private float m_SquareScale = 1.5f;
    [SerializeField] private Vector2 m_PositionOffset;

    private List<GameObject> m_SquareList = new List<GameObject>();

    private void Start()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SetSquaresPosition()
    {
        var l_squareRect = m_SquareList[0].GetComponent<RectTransform>().rect;
        var l_squareTransform = m_SquareList[0].GetComponent<RectTransform>();

        var l_offset = new Vector2
        {
            x = (l_squareRect.width * l_squareTransform.localScale.x + m_SquareOffset),
            y = (l_squareRect.height * l_squareTransform.localScale.y + m_SquareOffset)
        };

        var l_startPosition = GetFirstSquarePosition() + m_PositionOffset; // Add manual offset here
        int l_columnNum = 0;
        int l_rowNum = 0;

        foreach (var t_square in m_SquareList)
        {
            if (l_rowNum + 1 > m_CurrentBoard.m_Rows)
            {
                l_columnNum++;
                l_rowNum = 0;
            }

            var l_positionX = l_startPosition.x + (l_offset.x * l_columnNum);
            var l_positionY = l_startPosition.y - (l_offset.y * l_rowNum);

            t_square.GetComponent<RectTransform>().anchoredPosition = new Vector2(l_positionX, l_positionY);
            l_rowNum++;
        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var l_startPos = new Vector2(0f, 0f); // Centered on the canvas
        var l_squareRect = m_SquareList[0].GetComponent<RectTransform>().rect;
        var l_squareTransform = m_SquareList[0].GetComponent<RectTransform>();
        var l_squareSize = new Vector2(l_squareRect.width * l_squareTransform.localScale.x, l_squareRect.height * l_squareTransform.localScale.y);

        var l_midWidthPosition = ((m_CurrentBoard.m_Columns - 1) * l_squareSize.x) / 2;
        var l_midWidthHeight = ((m_CurrentBoard.m_Rows - 1) * l_squareSize.y) / 2;

        l_startPos.x = -l_midWidthPosition; // Center horizontally
        l_startPos.y = l_midWidthHeight;    // Center vertically

        return l_startPos;
    }

    private void SpawnGridSquares()
    {
        if (m_CurrentBoard == null) return;

        foreach (var t_squares in m_CurrentBoard.m_Board)
        {
            foreach (var t_squareLetter in t_squares.m_Row)
            {
                var squareObject = Instantiate(m_GridSquarePrefab, transform); // Create the square as a child of the canvas
                m_SquareList.Add(squareObject);

                var squareRect = squareObject.GetComponent<RectTransform>();
                squareObject.GetComponent<GridSquare>().SetLetter(t_squareLetter);
                squareRect.anchoredPosition = Vector2.zero;

                // Apply the manual scale factor
                squareRect.localScale = new Vector3(m_SquareScale, m_SquareScale, 1f);
            }
        }
    }
}


