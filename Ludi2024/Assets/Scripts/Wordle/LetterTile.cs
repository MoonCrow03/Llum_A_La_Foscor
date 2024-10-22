using System;
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

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnLetterClicked);
        }
        
        private void OnLetterClicked()
        {
            Board.Instance.OnLetterInput(letter);
        }
    }
}