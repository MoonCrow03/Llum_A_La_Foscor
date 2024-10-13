using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MakeWordsMinigame
{
    public class MakeWordsMinigameScript : MonoBehaviour
    {
        [Header("Word List")] 
        [SerializeField] private List<string> listOfWords;
        [SerializeField] private GameObject letterPrefab;
        [SerializeField] private Transform letterParent;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private int extraLetters;
        
        private List<char> availableLetters;
        private string selectedWord;
        private int numberOfLetters;

        private static readonly List<char> vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        private static readonly List<char> consonants = new List<char> {'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
    

        // Start is called before the first frame update
        void Start()
        {
            availableLetters = new List<char>();
            GenerateRandomLetters();
            CreateLetterObjects();
            CreatePlaceableSlots();
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
                GameObject slotObject = new GameObject("Slot " + i);
                slotObject.transform.SetParent(slotsParent);
                slotObject.AddComponent<PlaceableSlot>();
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
            Debug.Log("Word created!");
        }
    
    }
}
