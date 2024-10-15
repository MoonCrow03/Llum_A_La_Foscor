using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private BoardData m_BoardData;

    private string m_Word;

    private void OnEnable()
    {
        WordSearchEvents.OnCheckSquare += SquareSelected;
    }

    private void OnDisable()
    {
        WordSearchEvents.OnCheckSquare -= SquareSelected;
    }

    private void SquareSelected(string p_letter, Vector3 p_squarePosition, int p_squareIndex)
    {
        WordSearchEvents.SelectSquareMethod(p_squarePosition);
        m_Word += p_letter;
        CheckWord();
    }

    private void CheckWord()
    {
        foreach (var t_searchingWord in m_BoardData.m_SearchWords)
        {
            if (m_Word.Equals(t_searchingWord))
            {
                m_Word = string.Empty;
                return;
            }
        }
    }
}
