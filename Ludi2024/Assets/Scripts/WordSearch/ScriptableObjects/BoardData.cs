using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
[CreateAssetMenu(fileName = "BoardData", menuName = "WordSearch/BoardData", order = 1)]
public class BoardData : ScriptableObject
{
    [System.Serializable]
    public class SearchWord
    {
        public string m_Word;
    }

    [System.Serializable]
    public class BoardRow
    {
        public int m_Size;
        public string[] m_Row;

        public BoardRow(){}

        public BoardRow(int p_size)
        {
            CreateRow(p_size);
        }

        public void CreateRow(int p_size)
        {
            m_Size = p_size;
            m_Row = new string[p_size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < m_Size; i++)
            {
                m_Row[i] = " ";
            }
        }
    }

    public float m_TimeInSeconds;
    public int m_Columns;
    public int m_Rows;

    public BoardRow[] m_Board;
    public List<SearchWord> m_SearchWords = new List<SearchWord>();

    public void ClearWithEmptyString()
    {
        for (int i = 0; i < m_Columns; i++)
        {
            m_Board[i].ClearRow();
        }
    }

    public void CreateBoard()
    {
        m_Board = new BoardRow[m_Columns];
        for (int i = 0; i < m_Columns; i++)
        {
            m_Board[i] = new BoardRow(m_Rows);
        }
    }
}
