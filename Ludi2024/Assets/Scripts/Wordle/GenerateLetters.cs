using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wordle;

public class GenerateLetters : MonoBehaviour
{
    public Transform LetterParent;
    public GameObject LetterPrefab;
    public GameObject BackspacePrefab;
    public GameObject EnterPrefab;
    
    private static readonly char[] letters = new char[]
    {
        'A', 'B', 'C', 'Ç', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    public static Dictionary<char,LetterTile> LetterTiles = new Dictionary<char, LetterTile>();
    
    
    private void Start()
    {
        GenerateLetter();
    }

    public void GenerateLetter()
    {
        foreach (var letter in letters)
        {
            var letterTile = Instantiate(LetterPrefab, LetterParent).GetComponent<LetterTile>();
            letterTile.Letter = char.ToLower(letter);
            LetterTiles[char.ToLower(letter)] = letterTile;
        }
        var backspaceTile = Instantiate(BackspacePrefab, LetterParent).GetComponent<BackspaceTile>();
        backspaceTile.BackspaceChar = "Back";
        
        var enterTile = Instantiate(EnterPrefab, LetterParent).GetComponent<EnterTile>();
        enterTile.EnterChar = "Enter";
    }
    
    
}
