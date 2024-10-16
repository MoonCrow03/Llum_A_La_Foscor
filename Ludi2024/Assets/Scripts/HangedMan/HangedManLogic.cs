using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace HangedMan
{
    public class HangedManLogic : MonoBehaviour
    {
        [Header("Game Settings")] 
        [SerializeField] private List<string> wordList;
        [SerializeField] private int maxGuesses;
        
        [Header("Canvas Settings")]
        [SerializeField] private GameObject letterButtonPrefab;
        [SerializeField] private Transform letterParent;
        [SerializeField] private GameObject letterFromGuessWordPrefab;
        [SerializeField] private Transform letterFromGuessWordParent;
        
        private string wordToGuess;
        private Dictionary<char, bool> availableLetters;
        private Dictionary<char, GameObject> letterObjects;

        private static readonly List<char> letters = new List<char>
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private void Awake()
        {
            availableLetters = new Dictionary<char, bool>();
            letterObjects = new Dictionary<char, GameObject>();
        }

        private void Start()
        {
            SelectRandomWord();
            GenerateEmptyLetters();
            GenerateLetters();
        }

        private void GenerateLetters()
        {
            foreach (var letter in letters)
            {
                GameObject obj = Instantiate(letterButtonPrefab, letterParent);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = letter.ToString();
                Button button = obj.GetComponentInChildren<Button>();
                char letterCopy = letter;
                button.onClick.AddListener(() => OnLetterClicked(letterCopy));
                letterObjects.Add(letter, obj);
                availableLetters.Add(letter, true);
            }
        }
        
        private void GenerateEmptyLetters()
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                GameObject obj = Instantiate(letterFromGuessWordPrefab, letterFromGuessWordParent);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = "_";
            }
        }
        
        public void OnLetterClicked(char letterClicked)
        {
            if (availableLetters[letterClicked])
            {
                GuessLetter(letterClicked);
            }
        }

        private void SelectRandomWord()
        {
            wordToGuess = wordList[UnityEngine.Random.Range(0, wordList.Count)];
        }

        private void GuessLetter(char letterGuessed)
        {
            availableLetters[letterGuessed] = false;
            DisableLetterInteraction(letterGuessed);
            
            if (wordToGuess.Contains(letterGuessed))
            {
                UpdateShownWord(letterGuessed);
            }
            else
            {
                ChangeLetterColor(letterGuessed, Color.red);
                maxGuesses--;
            }
        }

        private void DisableLetterInteraction(char letterGuessed)
        {
            letterObjects[letterGuessed].GetComponentInChildren<Button>().interactable = false;
        }

        private void ChangeLetterColor(char letterGuessed, Color color)
        {
            letterObjects[letterGuessed].GetComponentInChildren<TextMeshProUGUI>().color = color;
        }

        private void UpdateShownWord(char letterGuessed)
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == letterGuessed)
                {
                    Transform letter = letterFromGuessWordParent.GetChild(i);
                    letter.GetComponentInChildren<TextMeshProUGUI>().text = letterGuessed.ToString();
                }
            }
        }
        
        
    }
}