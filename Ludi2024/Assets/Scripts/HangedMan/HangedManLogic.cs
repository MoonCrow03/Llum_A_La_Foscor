using System;
using System.Collections.Generic;
using UnityEngine;

namespace HangedMan
{
    public class HangedManLogic : MonoBehaviour
    {
        [Header("Game Settings")] 
        private string wordToGuess;
        private int maxGuesses;

        private Dictionary<char, bool> availableLetters;

        private void Awake()
        {
            availableLetters = new Dictionary<char, bool>();
        }

        private void Start()
        {
            
        }

        private void GuessLetter(char letterGuessed)
        {
            availableLetters[letterGuessed] = true;
        }
    }
}