using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingWordList : MonoBehaviour
{
    [Header("Searching Word List Settings")]
    [SerializeField] private BoardData m_BoardData;
    [SerializeField] private GameObject m_SearchingWordPrefab;
    [SerializeField] private float m_Offset = 0.0f;
    [SerializeField] private int m_MaxColumns = 2;
    [SerializeField] private int m_MaxRows = 5;

    private int m_Columns = 2;
    private int m_Rows = 5;
    private int m_WordsNumber = 0;

    private List<GameObject> m_Words = new List<GameObject>();

    private void Start()
    {
        m_WordsNumber = m_BoardData.m_SearchWords.Count;

        if(m_WordsNumber < m_Columns)
        {
            m_Rows = 1;
        }
        else
        {
            CalculateColumnsAndRowNumbers();
        }

        CreateWordsObjects();
        SetWordsPosition();
    }

    private void CalculateColumnsAndRowNumbers()
    {
        do
        {
            m_Columns++;
            m_Rows = m_WordsNumber / m_Columns;
        }
        while (m_Rows >= m_MaxRows);

        if (m_Columns > m_MaxColumns)
        {
            m_Columns = m_MaxColumns;
            m_Rows = m_WordsNumber / m_Columns;
        }
    }

    private bool TryIncreaseColumnNumber()
    {
        m_Columns++;
        m_Rows = m_WordsNumber / m_Columns;

        if (m_Columns > m_MaxColumns)
        {
            m_Columns = m_MaxColumns;
            m_Rows = m_WordsNumber / m_Columns;

            return false;
        }

        if (m_WordsNumber % m_Columns > 0)
        {
            m_Rows++;
        }

        return true;
    }

    private void CreateWordsObjects()
    {
        var l_squareScale = GetSquareScale(new Vector3(1f, 1f, 0.1f));

        for (int i = 0; i < m_WordsNumber; i++)
        {
            m_Words.Add(Instantiate(m_SearchingWordPrefab) as GameObject);
            m_Words[i].transform.SetParent(transform);
            m_Words[i].GetComponent<RectTransform>().localScale = l_squareScale;
            m_Words[i].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);

            m_Words[i].GetComponent<SearchingWord>().SetWord(m_BoardData.m_SearchWords[i].m_Word);
        }
    }

    private Vector3 GetSquareScale(Vector3 p_defaultScale)
    {
        var l_finalScale = p_defaultScale;
        var l_adjustment = 0.01f;

        while (ShouldScaleDown(l_finalScale))
        {
            l_finalScale.x -= l_adjustment;
            l_finalScale.y -= l_adjustment;

            if (l_finalScale.x <= 0f || l_finalScale.y <= 0f)
            {
                l_finalScale.x = l_adjustment;
                l_finalScale.y = l_adjustment;

                return l_finalScale;
            }
        }

        return l_finalScale;
    }

    private bool ShouldScaleDown(Vector3 p_targetSquare)
    {
        var l_squareRect = m_SearchingWordPrefab.GetComponent<RectTransform>();
        var l_parentRect = GetComponent<RectTransform>();

        var l_squareSize = new Vector2(0f, 0f);

        l_squareSize.x = l_squareRect.rect.width * p_targetSquare.x + m_Offset;
        l_squareSize.y = l_squareRect.rect.height * p_targetSquare.y + m_Offset;

        var l_totalSquaresHeight = l_squareSize.y * m_Rows;

        // Make sure all the squares fit in the parent rectangle area
        if(l_totalSquaresHeight > l_parentRect.rect.height)
        {
            while (l_totalSquaresHeight > l_parentRect.rect.height)
            {
                if (TryIncreaseColumnNumber())
                {
                    l_totalSquaresHeight = l_squareSize.y * m_Rows;
                }
                else
                {
                    return true;
                }
            }
        }

        var l_totalSquaresWidth = l_squareSize.x * m_Columns;

        if (l_totalSquaresWidth > l_parentRect.rect.width)
        {
            return true;
        }

        return false;
    }

    private void SetWordsPosition()
    {
        var l_squareRect = m_Words[0].GetComponent<RectTransform>();
        var l_wordOffset = new Vector2
        {
            x = l_squareRect.rect.width * l_squareRect.transform.localScale.x + m_Offset,
            y = l_squareRect.rect.height * l_squareRect.transform.localScale.y + m_Offset
        };

        int l_columnNumber = 0;
        int l_rowNumber = 0;

        var l_startPosition = GetFirstSquarePosition();

        foreach (var t_word in m_Words)
        {
            if (l_columnNumber + 1 > m_Columns)
            {
                l_columnNumber = 0;
                l_rowNumber++;
            }

            var l_positionX = l_startPosition.x + l_wordOffset.x * l_columnNumber;
            var l_positionY = l_startPosition.y - l_wordOffset.y * l_rowNumber;

            t_word.GetComponent<RectTransform>().localPosition = new Vector2(l_positionX, l_positionY);
            l_columnNumber++;
        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var l_startPosition = new Vector2(0f, 0f);  // Start at top-left

        var l_squareRect = m_Words[0].GetComponent<RectTransform>();
        var l_parentRect = GetComponent<RectTransform>();

        var l_squareSize = new Vector2
        {
            x = l_squareRect.rect.width * l_squareRect.transform.localScale.x + m_Offset,
            y = l_squareRect.rect.height * l_squareRect.transform.localScale.y + m_Offset
        };

        // Position the first word at the top-left corner of the parent container
        l_startPosition.x = l_parentRect.rect.xMin + l_squareSize.x / 2;
        l_startPosition.y = l_parentRect.rect.yMax - l_squareSize.y / 2;  // yMax to start from the top

        return l_startPosition;
    }
}
