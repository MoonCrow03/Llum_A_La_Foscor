using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wordle
{
    public class LetterTile : MonoBehaviour
    {
        private char letter;
        
        public char Letter
        {
            get => letter;
            set
            {
                letter = value;
                GetComponentInChildren<TMPro.TextMeshProUGUI>().text = letter.ToString();
            }
        }

        private Image fillImage;
        private Outline outline;
        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnLetterClicked);
            fillImage = GetComponent<Image>();
            outline = GetComponent<Outline>();
            textMeshProUGUI = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        
        private void OnLetterClicked()
        {
            Board.Instance.OnLetterInput(letter);
        }

        public void SetTileState(Tile.TileStates correctState)
        {
            fillImage.color = correctState.FillColor;
            outline.effectColor = correctState.OutlineColor;
        }
        
        public void SetTextToWhite()
        {
            textMeshProUGUI.color = Color.white;
        }
    }
}