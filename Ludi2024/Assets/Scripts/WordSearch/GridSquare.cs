using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GridSquare : MonoBehaviour
{
    /*public int m_SquareIndex { get; set; }

    private AlphabetData.LetterData m_NormalLetterData;
    private AlphabetData.LetterData m_SelectedLetterData;
    private AlphabetData.LetterData m_WrongLetterData;

    private SpriteRenderer m_DisplayedImage;

    private void Start()
    {
        m_DisplayedImage = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(AlphabetData.LetterData p_normalLetterData, AlphabetData.LetterData p_selectedLetterData, AlphabetData.LetterData p_correctLetterData)
    {
        m_NormalLetterData = p_normalLetterData;
        m_SelectedLetterData = p_selectedLetterData;
        m_WrongLetterData = p_correctLetterData;

        GetComponent<SpriteRenderer>().sprite = m_NormalLetterData.m_Image;
    }*/



    [Header("Grid Square Settings")]
    [SerializeField] private Color m_NormalColor;
    [SerializeField] private Color m_SelectedColor;
    [SerializeField] private Color m_CorrectColor;

    public string m_Letter;
    private Image m_Image;
    private TextMeshProUGUI m_Text;

    public int m_SquareIndex { get; set; }

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLetter(string p_letter)
    {
        m_Letter = p_letter;
        m_Text.text = m_Letter;
    }

    public void SetNormalColor()
    {
        m_Image.color = m_NormalColor;
    }

    public void SetSelectedColor()
    {
        m_Image.color = m_SelectedColor;
    }

    public void SetCorrectColor()
    {
        m_Image.color = m_CorrectColor;
    }
}
