using System;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;
using Random = System.Random;

namespace HangedMan
{
    public class HangedManLogic : MonoBehaviour
    {
        [Header("Game Settings")] 
        [SerializeField] private List<string> wordList;
        [SerializeField] private int maxGuesses;
        [SerializeField] private float timeLeft;
        [SerializeField] private bool isTutorial;
        [SerializeField] private float pointsMultiplier = 1.0f;

        [Header("Canvas Settings")]
        [SerializeField] private GameObject letterButtonPrefab;
        [SerializeField] private Transform letterParent;
        [SerializeField] private GameObject letterFromGuessWordPrefab;
        [SerializeField] private Transform letterFromGuessWordParent;
        

        [Header("Scenes")]
        [SerializeField] private TextMeshProUGUI clockText;
        
        [Header("Audio")]
        public EventReference AudioEventWin;
        public EventReference AudioEventLose;
        
        private string wordToGuess;
        private Dictionary<char, bool> availableLetters;
        private Dictionary<char, GameObject> letterObjects;
        private TimeLimit timeLimit;
        private bool gameCompleted = false;

        private static readonly List<char> letters = new List<char>
        {
            'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z' 
        };
        
        private FMOD.Studio.EventInstance AudioInstanceWin;
        private FMOD.Studio.EventInstance AudioInstanceLose;

        private void Awake()
        {
            availableLetters = new Dictionary<char, bool>();
            letterObjects = new Dictionary<char, GameObject>();
        }

        private void Start()
        {
            AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(AudioEventWin);
            AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(AudioEventLose);
            if (!isTutorial)
            {
                timeLimit = new TimeLimit(this);
                timeLimit.StartTimer(timeLeft, GameFailed);
            }
            
            SelectRandomWord();
            GenerateEmptyLetters();
            GenerateLetters();
        }

        private void Update()
        {
            UpdateClockText();
        }

        private void UpdateClockText()
        {
            if (timeLimit.GetTimeRemaining() <= 0)
            {
                clockText.text = "00:00";
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(timeLimit.GetTimeRemaining());
                clockText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }
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
            if (gameCompleted) return;
            
            foreach (var letter in availableLetters)
            {
                DisableLetterInteraction(letter.Key);
            }
            
            timeLimit.StopTimer();
            
            AudioInstanceLose.start();
            GameEvents.TriggerSetEndgameMessage("Has perdut!", false, 0);
        }

        private void GameWon()
        {
            gameCompleted = true;
            foreach (var letter in availableLetters)
            {
                DisableLetterInteraction(letter.Key);
            }
            timeLimit.StopTimer();
            AudioInstanceWin.start();

            int l_stars = 3;
            GameEvents.TriggerSetEndgameMessage("Felicitats!", true, l_stars);
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

        private void OnEnable()
        {
            TutorialText.OnTutorialFinished += StartTimer;
        }
        
        private void OnDisable()
        {
            TutorialText.OnTutorialFinished -= StartTimer;
        }

        private void StartTimer()
        {
            timeLimit = new TimeLimit(this);
            timeLimit.StartTimer(timeLeft, GameFailed);
        }
        
        private void OnDestroy()
        {
            AudioInstanceWin.release();
            AudioInstanceLose.release();
        }
    }
}