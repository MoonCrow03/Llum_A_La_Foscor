using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace HangedMan
{
    public class HangedManLogic : MonoBehaviour
    {
        [Header("Game Settings")] 
        private List<string> wordsToGuess;
        private int maxGuesses;
        
        private string wordToGuess;
        private Dictionary<char, bool> availableLetters;

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
            wordToGuess = wordsToGuess[UnityEngine.Random.Range(0, wordsToGuess.Count)];
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