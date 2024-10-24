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
        
        [Header("Scenes")]
        [SerializeField] private string worldScene;
        [SerializeField] private ELevelsCompleted levelCompleted;
        
        private string wordToGuess;
        private Dictionary<char, bool> availableLetters;
        private Dictionary<char, GameObject> letterObjects;

        private static readonly List<char> letters = new List<char>
        {
            'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z' 
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
            char lowerLetter = char.ToLower(letterClicked);
            if (availableLetters[lowerLetter])
            {
                GuessLetter(lowerLetter);
            }
        }

        private void SelectRandomWord()
        {
            wordToGuess = wordList[UnityEngine.Random.Range(0, wordList.Count)].ToLower();
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

            if (maxGuesses == 0)
            {
                GameFailed();
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
                if (!wordToGuess[i].Equals(letterGuessed)) continue;
                Transform letter = letterFromGuessWordParent.GetChild(i);
                letter.GetComponentInChildren<TextMeshProUGUI>().text = i == 0 ? letterGuessed.ToString().ToUpper() : letterGuessed.ToString();
            }
            
            if (IsWordGuessed())
            {
                GameWon();
            }
        }

        private void GameFailed()
        {
            Debug.Log("Failed! The word was: " + wordToGuess);

            foreach (var letter in availableLetters)
            {
                DisableLetterInteraction(letter.Key);
            }

            EngameUI.TriggerSetEndgameMessage("Has perdut!", false);
            EngameUI.TriggerEnableEndgamePanel(true);
        }

        private void GameWon()
        {
            Debug.Log("Hangman completed! The word was: " + wordToGuess);
            foreach (var letter in availableLetters)
            {
                DisableLetterInteraction(letter.Key);
            }

            EngameUI.TriggerSetEndgameMessage("Felicitats!", true);
            EngameUI.TriggerEnableEndgamePanel(true);
            //GameManager.Instance.SetMiniGameCompleted(levelCompleted);
        }

        private bool IsWordGuessed()
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (letterFromGuessWordParent.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text.Equals("_"))
                {
                    return false;
                }
            }

            return true;
        }
    }
}