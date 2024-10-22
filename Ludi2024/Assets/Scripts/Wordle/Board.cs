using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wordle
{
    public class Board : MonoBehaviour
    {
        [Header("Word Settings")]
        public string solutionWord;
        public int wordLength;
        
        private const int EASY_WORD_LENGTH = 3;
        private const int MEDIUM_WORD_LENGTH = 4;
        private const int HARD_WORD_LENGTH = 5;
        private const int VERY_HARD_WORD_LENGTH = 6;
        private const int EXPERT_WORD_LENGTH = 7;
        
        [Header("Tile States")]
        public Tile.TileStates EmptyState;
        public Tile.TileStates OccupiedState;
        public Tile.TileStates CorrectState;
        public Tile.TileStates WrongSpot;
        public Tile.TileStates IncorrectState;
        
        private Row[] rows;
        private string[] validWords;
        private int rowIndex;
        private int columnIndex;
        
        private static readonly string[] SEPARATOR = new string[] { "\r\n", "\r", "\n" };
        
        private void Awake()
        {
            rows = GetComponentsInChildren<Row>();
        }

        private void Start()
        {
            LoadWordsFromTxt();
        }

        private void LoadWordsFromTxt()
        {
            switch (wordLength)
            {
                case EASY_WORD_LENGTH:
                    TextAsset textFile = Resources.Load("dictionary_3_letters_final") as TextAsset;
                    validWords = textFile.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case MEDIUM_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_4_letters_final") as TextAsset;
                    validWords = textFile.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case HARD_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_5_letters_final") as TextAsset;
                    validWords = textFile.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case VERY_HARD_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_6_letters_final") as TextAsset;
                    validWords = textFile.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case EXPERT_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_7_letters_final") as TextAsset;
                    validWords = textFile.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        

        private void Update()
        {
            Row currentRow = rows[rowIndex];
            
            if (InputManager.Instance.Backspace.Tap)
            {
                columnIndex = Mathf.Max(columnIndex - 1, 0);
                currentRow.Tiles[columnIndex].SetLetter('\0');
                currentRow.Tiles[columnIndex].SetTileState(EmptyState);
            }
            else if (columnIndex >= rows[rowIndex].Tiles.Length)
            {
                if (InputManager.Instance.Enter.Tap)
                {
                    SubmitRow(currentRow);
                    
                }
            }
            else
            {
                foreach (char c in Input.inputString)
                {
                    currentRow.Tiles[columnIndex].SetLetter(c);
                    currentRow.Tiles[columnIndex].SetTileState(OccupiedState);
                    
                    columnIndex++;
                    break;
                }
            }
        }

        private void SubmitRow(Row row)
        {
            if (!IsValidWord(row.word))
            {
                return;
            }
            
            string remaining = solutionWord;
            for (int i = 0; i < row.Tiles.Length; i++)
            {
                Tile tile = row.Tiles[i];
                
                if (tile.Letter == solutionWord[i])
                {
                    tile.SetTileState(CorrectState);
                    remaining = remaining.Remove(1, 1);
                    remaining = remaining.Insert(1, " ");
                }
                else if (!solutionWord.Contains(tile.Letter))
                {
                    tile.SetTileState(IncorrectState);
                }
            }

            for (int i = 0; i < row.Tiles.Length; i++)
            {
                Tile tile = row.Tiles[i];

                if (tile.State != CorrectState && tile.State != IncorrectState)
                {
                    if (remaining.Contains(tile.Letter))
                    {
                        tile.SetTileState(WrongSpot);
                        
                        int index = remaining.IndexOf(tile.Letter);
                        remaining = remaining.Remove(index, 1);
                        remaining = remaining.Insert(index, " ");
                    }
                    else
                    {
                        tile.SetTileState(IncorrectState);
                    }
                }
            }
            /*for (int i = 0; i < row.Tiles.Length; i++)
            {
                Tile tile = row.Tiles[i];

                if (tile.Letter == solutionWord[i])
                {
                    tile.SetTileState(CorrectState);
                }
                else if (solutionWord.Contains(tile.Letter.ToString()))
                {
                    tile.SetTileState(WrongSpot);
                }
                else
                {
                    tile.SetTileState(IncorrectState);
                }
            }*/
            
            rowIndex++;
            columnIndex = 0;

            if (rowIndex >= rows.Length)
            {
                enabled = false;
            }
        }

        private bool IsValidWord(string word)
        {
            if (word == solutionWord) return true;
            
            for (int i = 0; i < validWords.Length; i++)
            {
                if (string.Equals(word, validWords[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}