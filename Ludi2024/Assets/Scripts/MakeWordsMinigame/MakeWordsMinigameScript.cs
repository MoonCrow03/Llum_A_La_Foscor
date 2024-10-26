using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Random = UnityEngine.Random;

namespace MakeWordsMinigame
{
    public class MakeWordsMinigameScript : MonoBehaviour
    {
        
        [Header("Word List")] 
        [SerializeField] private List<string> listOfWords;
        [SerializeField] private GameObject letterPrefab;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform letterParent;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private int extraLetters;

        [Header("Timer Settings")] 
        [SerializeField] private float time;
        
        [Header("Scene Settings")]
        [SerializeField] private Scenes levelCompleted;
        [SerializeField] private TextMeshProUGUI clockText;
        
        [Header("Audio")]
        public EventReference AudioEventWin;
        public EventReference AudioEventLose;
        
        private List<char> availableLetters;
        private string selectedWord;
        private int numberOfLetters;
        private TimeLimit timeLimit;
        private bool gameCompleted = false;

        private static readonly List<char> vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        private static readonly List<char> consonants = new List<char> {'b', 'c', 'ç', 'd', 'f', 'g', 'h', 'j', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v'};
        
        private FMOD.Studio.EventInstance AudioInstanceWin;
        private FMOD.Studio.EventInstance AudioInstanceLose;
        
        // Start is called before the first frame update
        void Start()
        {
            timeLimit = new TimeLimit(this);
            timeLimit.StartTimer(time, OnGameFailed);
            AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(AudioEventWin);
            AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(AudioEventLose);
            
            availableLetters = new List<char>();
            GenerateRandomLetters();
            ResizeLayouts();
            CreateLetterObjects();
            CreatePlaceableSlots();
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

        private void OnEnable()
        {
            LetterFormDrop.OnLetterDropped += CheckWordFormed;
        }
        
        private void OnDisable()
        {
            LetterFormDrop.OnLetterDropped -= CheckWordFormed;
        }

        private void GenerateRandomLetters()
        {
            selectedWord = listOfWords[Random.Range(0, listOfWords.Count)];
            numberOfLetters = selectedWord.Length;
            List<char> selectedWordLetters = selectedWord.ToList();

            for (int i = 0; i < numberOfLetters + extraLetters; i++)
            {
                if (i < numberOfLetters)
                {
                    availableLetters.Add(selectedWordLetters[i]);
                }
                else
                {
                    availableLetters.Add(Random.Range(0, 2) == 0 ? GetRandomVowel() : GetRandomConsonant());
                }
            }
        }

        private void CreateLetterObjects()
        {
            ShuffleList(ref availableLetters);
            foreach (char letter in availableLetters)
            {
                GameObject letterObject = Instantiate(letterPrefab, letterParent);
                TextMeshProUGUI letterText = letterObject.GetComponentInChildren<TextMeshProUGUI>();
                letterText.text = letter.ToString();
            }
        }

        private void CreatePlaceableSlots()
        {
            for (int i = 0; i < selectedWord.Length; i++)
            {
                Instantiate(slotPrefab, slotsParent);
            }
        }

        private void CheckWordFormed()
        {
            string wordFormed = "";
            foreach (Transform slot in slotsParent)
            {
                if (slot.childCount > 0)
                {
                    TextMeshProUGUI letterText = slot.GetComponentInChildren<TextMeshProUGUI>();
                    wordFormed += letterText.text;
                }
            }
            
            if (wordFormed == selectedWord)
            {
                OnWordCreated();
            }
            
        }
    
        private char GetRandomVowel()
        {
            return vowels[Random.Range(0, vowels.Count)];
        }
    
        private char GetRandomConsonant()
        {
            return consonants[Random.Range(0, consonants.Count)];
        }

        private void OnWordCreated()
        {
            timeLimit.StopTimer();
            AudioInstanceWin.start();
            gameCompleted = true;
            GameManager.Instance.SetMiniGameCompleted(levelCompleted);
            GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
        }

        private void OnGameFailed()
        {
            if (gameCompleted) return;
            timeLimit.StopTimer();
            AudioInstanceLose.start();
            GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
        }

        private void ShuffleList(ref List<char> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                char temp = list[i];
                int randomIndex = Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        private void ResizeLayouts()
        {
            GridLayoutGroup slotGridLayout = slotsParent.GetComponent<GridLayoutGroup>();
            GridLayoutGroup letterGridLayout = letterParent.GetComponent<GridLayoutGroup>();

            // Get the width of the parent RectTransform (for one row)
            RectTransform parentRect = slotsParent.GetComponent<RectTransform>();
            float parentWidth = parentRect.rect.width;
            float parentHeight = parentRect.rect.height;

            // Number of slots (equal to the length of the selected word)
            int numberOfSlots = selectedWord.Length;

            // Calculate the maximum possible slot size, constrained by either width or height
            float maxSlotSizeByWidth = parentWidth / numberOfSlots;
            float maxSlotSizeByHeight = parentHeight;

            // Choose the smaller value to ensure the slots fit horizontally and are square
            float slotSize = Mathf.Min(maxSlotSizeByWidth, maxSlotSizeByHeight);

            // Set both the width and height of each slot to the calculated size to make them squares
            slotGridLayout.cellSize = new Vector2(slotSize, slotSize);
            letterGridLayout.cellSize = new Vector2(slotSize, slotSize);
        }

        private void OnDestroy()
        {
            AudioInstanceWin.release();
            AudioInstanceLose.release();
        }
    }
}
