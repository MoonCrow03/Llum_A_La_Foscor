using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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
        
        private List<char> availableLetters;
        private string selectedWord;
        private int numberOfLetters;

        private static readonly List<char> vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        private static readonly List<char> consonants = new List<char> {'b', 'c', 'd', 'f', 'g', 'h', 'j', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v'};
    

        // Start is called before the first frame update
        void Start()
        {
            availableLetters = new List<char>();
            GenerateRandomLetters();
            CreateLetterObjects();
            CreatePlaceableSlots();
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
            BasicSceneChanger.ChangeScene("World Scene");
            Debug.Log("Word created!");
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
    }
}
