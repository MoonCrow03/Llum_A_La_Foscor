using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static WordSearchEvents;


public class GridSquare : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [Header("Grid Square Settings")]
    [SerializeField] private Color m_NormalColor;
    [SerializeField] private Color m_SelectedColor;
    [SerializeField] private Color m_CorrectColor;

    public string m_Letter;
    private Image m_Image;
    private TextMeshProUGUI m_Text;
    private bool m_IsSelected;
    private bool m_IsClicked;
    private bool m_IsCorrect;
    private int m_Index = -1;

    public int m_SquareIndex { get; set; }

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Text = GetComponentInChildren<TextMeshProUGUI>();

        m_IsSelected = false;
        m_IsClicked = false;
        m_IsCorrect = false;
    }

    private void OnEnable()
    {
        WordSearchEvents.OnEnableSquareSelection += OnEnableSquareSelection;
        WordSearchEvents.OnDisableSquareSelection += OnDisableSquareSelection;
        WordSearchEvents.OnSelectSquare += SelectSquare;
    }

    private void OnDisable()
    {
        WordSearchEvents.OnEnableSquareSelection -= OnEnableSquareSelection;
        WordSearchEvents.OnDisableSquareSelection -= OnDisableSquareSelection;
        WordSearchEvents.OnSelectSquare -= SelectSquare;
    }

    private void OnEnableSquareSelection()
    {
        m_IsClicked = true;
        m_IsSelected = false;
    }

    private void OnDisableSquareSelection()
    {
        m_IsClicked = false;
        m_IsSelected = false;

        m_Image.color = m_IsCorrect ? m_CorrectColor : m_NormalColor;
    }

    private void SelectSquare(Vector3 p_position)
    {
        if (gameObject.transform.position == p_position)
        {
            m_Image.color = m_SelectedColor;
        }
    }

    private void CheckSquare()
    {
        if (!m_IsSelected && m_IsClicked)
        {
            m_IsSelected = true;

            WordSearchEvents.CheckSquareMethod(m_Letter, gameObject.transform.position, m_Index);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnEnableSquareSelection();
        WordSearchEvents.EnableSquareSelectionMethod();
        CheckSquare();
        m_Image.color = m_SelectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CheckSquare();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        WordSearchEvents.ClearSelectionMethod();
        WordSearchEvents.DisableSquareSelectionMethod();
    }

    public void SetLetter(string p_letter)
    {
        m_Letter = p_letter;
        m_Text.text = m_Letter;
    }

    public void SetIndex(int p_index)
    {
        m_Index = p_index;
    }

    public int GetIndex()
    {
        return m_Index;
    }
}
