using System;
using UnityEngine;
using UnityEngine.UI;

namespace Wordle
{
    public class BackspaceTile : MonoBehaviour
    {
        private string backspaceChar = "Back";

        public string BackspaceChar
        {
            get => backspaceChar;
            set
            {
                backspaceChar = value;
                GetComponentInChildren<TMPro.TextMeshProUGUI>().text = backspaceChar;
            }
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnBackspaceClicked);
        }

        private void OnBackspaceClicked()
        {
            Board.Instance.OnBackspaceInput();
        }
    }
}