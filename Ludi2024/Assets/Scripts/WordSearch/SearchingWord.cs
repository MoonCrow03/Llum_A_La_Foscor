using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchingWord : MonoBehaviour
{
    [SerializeField] private Image m_Line;
    private TextMeshProUGUI m_Text;
    private string m_Word;

    private void Awake()
    {
        m_Text = GetComponentInChildren<TextMeshProUGUI>();

        m_Line.enabled = false;
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        WordSearchEvents.OnCorrectWord += CorrectWord;
    }

    private void OnDisable()
    {
        WordSearchEvents.OnCorrectWord -= CorrectWord;
    }

    public void SetWord(string p_word)
    {
        m_Word = p_word;
        m_Text.text = p_word;
    }

    private void CorrectWord(string p_word, List<int> p_squareIndexes)
    {
        if (p_word == m_Word)
        {
            m_Line.enabled = true;
        }
    }
}
