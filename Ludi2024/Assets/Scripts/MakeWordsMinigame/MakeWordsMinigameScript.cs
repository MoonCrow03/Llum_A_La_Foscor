using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakeWordsMinigame
{
    public class MakeWordsMinigameScript : MonoBehaviour
    {
        [Header("Word List")] 
        [SerializeField] private List<string> listOfWords;
        [SerializeField] private int numberOfLetters;

        private List<char> availableLetters;

        private string selectedWord;

        private static readonly List<char> vowels = new List<char> {'a', 'e', 'i', 'o', 'u'};
        private static readonly List<char> consonants = new List<char> {'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
    

        // Start is called before the first frame update
        void Start()
        {
            GenerateRandomLetters();
        }

        private void GenerateRandomLetters()
        {
            selectedWord = listOfWords[Random.Range(0, listOfWords.Count)];
            List<char> selectedWordLetters = selectedWord.ToList();

            for (int i = 0; i < numberOfLetters; i++)
            {
                if (i < selectedWordLetters.Count)
                {
                    availableLetters.Add(selectedWordLetters[i]);
                }
                else
                {
                    availableLetters.Add(Random.Range(0, 2) == 0 ? GetRandomVowel() : GetRandomConsonant());
                }
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

        private void CreateWord()
        {
        }

        private void OnWordCreated()
        {
            
        }
    
    }
}
