using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace HangedMan
{
    public class HangedManLogic : MonoBehaviour
    {
        [Header("Game Settings")] 
        [SerializeField] private List<string> wordList;
        [SerializeField] private int maxGuesses;
        
        private string wordToGuess;
        private Dictionary<char, bool> availableLetters;

        private static readonly List<char> letters = new List<char>
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private void Awake()
        {
            availableLetters = new Dictionary<char, bool>();
        }

        private void Start()
        {
            SelectRandomWord();
        }

        private void SelectRandomWord()
        {
            wordToGuess = wordList[UnityEngine.Random.Range(0, wordList.Count)];
        }

        private void GuessLetter(char letterGuessed)
        {
            availableLetters[letterGuessed] = true;
            if (wordToGuess.Contains(letterGuessed))
            {
                UpdateShownWord(letterGuessed);
            }
            else
            {
                maxGuesses--;
            }
        }

        private void UpdateShownWord(char letterGuessed)
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == letterGuessed)
                {
                    // Update shown word
                }
            }
        }
        
        
    }
}