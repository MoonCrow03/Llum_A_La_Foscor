using System;
using System.Collections.Generic;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Random = UnityEngine.Random;

namespace Wordle
{
    public class Board : MonoBehaviour
    {
        [Header("Wordle Settings")]
        public List<string> listOfPossibleSolutions = new List<string>();
        [SerializeField] private float timeToBeat;
        [SerializeField] private float pointsMultiplier = 1.0f;
        
        [NonSerialized] public int WordLength;

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
        

        [Header("Scene Settings")] [SerializeField]
        private TextMeshProUGUI timeLeft;
        [SerializeField] private Scenes levelCompleted;
        
        [Header("Audio")]
        public EventReference AudioEventWin;
        public EventReference AudioEventLose;
        
        
        private Row[] rows;
        private string[] validWords;
        private string solutionWord;
        private int rowIndex;
        private int columnIndex;
        private bool gameCompleted = false;
        
        private TimeLimit timeLimit;
        
        private FMOD.Studio.EventInstance AudioInstanceWin;
        private FMOD.Studio.EventInstance AudioInstanceLose;

        
        private static readonly string[] SEPARATOR = new string[] { "\r\n", "\r", "\n" };

        private static Board instance;

        public static Board Instance => instance;
        private void Awake()
        {
            rows = GetComponentsInChildren<Row>();
            solutionWord = listOfPossibleSolutions[Random.Range(0, listOfPossibleSolutions.Count)];
            WordLength = solutionWord.Length;
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            AudioInstanceWin = FMODUnity.RuntimeManager.CreateInstance(AudioEventWin);
            AudioInstanceLose = FMODUnity.RuntimeManager.CreateInstance(AudioEventLose);
            timeLimit = new TimeLimit(this);
            timeLimit.StartTimer(timeToBeat, EndGameFailed);
            LoadWordsFromTxt();
        }

        private void LoadWordsFromTxt()
        {
            switch (WordLength)
            {
                case EASY_WORD_LENGTH:
                    TextAsset textFile = Resources.Load("dictionary_3_letters_final") as TextAsset;
                    validWords = textFile?.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case MEDIUM_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_4_letters_final") as TextAsset;
                    validWords = textFile?.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case HARD_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_5_letters_final") as TextAsset;
                    validWords = textFile?.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case VERY_HARD_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_6_letters_final") as TextAsset;
                    validWords = textFile?.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                case EXPERT_WORD_LENGTH:
                    textFile = Resources.Load("dictionary_7_letters_final") as TextAsset;
                    validWords = textFile?.text.Split(SEPARATOR, System.StringSplitOptions.None);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnLetterInput(char letter)
        {
            Row currentRow = rows[rowIndex];
            if (columnIndex < currentRow.Tiles.Length)
            {
                currentRow.Tiles[columnIndex].SetLetter(letter);
                currentRow.Tiles[columnIndex].SetTileState(OccupiedState);
                columnIndex++;
            }
            
            /*if (columnIndex >= currentRow.Tiles.Length)
            {
                SubmitRow(currentRow);
            }*/
        }
        

        private void Update()
        {
            Row currentRow = rows[rowIndex];
            
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeLimit.GetTimeRemaining());
            timeLeft.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            
            if (timeLeft.text == "00:00")
            {
               timeLeft.text = "00:00";
            }
            
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
                    if (c == '\u0008') continue; // Caracter basura generado por mantener la tecla de Backspace
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
                    UpdateLetterTileColor(tile.Letter, CorrectState);
                    remaining = remaining.Remove(1, 1);
                    remaining = remaining.Insert(1, " ");
                }
                else if (!solutionWord.Contains(tile.Letter))
                {
                    tile.SetTileState(IncorrectState);
                    UpdateLetterTileColor(tile.Letter, IncorrectState);
                    UpdateLetterTextToWhite(tile.Letter);
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
                        UpdateLetterTileColor(tile.Letter, WrongSpot);
                        
                        int index = remaining.IndexOf(tile.Letter);
                        remaining = remaining.Remove(index, 1);
                        remaining = remaining.Insert(index, " ");
                    }
                    else
                    {
                        tile.SetTileState(IncorrectState);
                        UpdateLetterTileColor(tile.Letter, IncorrectState);
                        UpdateLetterTextToWhite(tile.Letter);
                    }
                }
            }

            if (CheckWordGuessed(ref row))
            {
                gameCompleted = true;
                timeLimit.StopTimer();
                GameManager.Instance.SetMiniGameCompleted(levelCompleted);
                AudioInstanceWin.start();
                GameManager.Instance.Points += timeLimit.GetPoints(pointsMultiplier);
                GameEvents.TriggerSetEndgameMessage("Has guanyat!", true);
            }
            
            rowIndex++;
            columnIndex = 0;

            if (rowIndex >= rows.Length)
            {
                gameCompleted = true;
                timeLimit.StopTimer();
                enabled = false;
                AudioInstanceLose.start();
                GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
            }
        }

        private void UpdateLetterTileColor(char tileLetter, Tile.TileStates correctState)
        {
            if (GenerateLetters.LetterTiles.TryGetValue(tileLetter, out var letterTile))
            {
                letterTile.SetTileState(correctState);
            }
        }
        
        private void UpdateLetterTextToWhite(char tileLetter)
        {
            if (GenerateLetters.LetterTiles.TryGetValue(tileLetter, out var letterTile))
            {
                letterTile.SetTextToWhite();
            }
        }
        
        private void EndGameFailed()
        {
            if (gameCompleted) return;
            AudioInstanceLose.start();
            GameEvents.TriggerSetEndgameMessage("Has perdut!", false);
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

        private bool CheckWordGuessed(ref Row row)
        {
            string formedWord = "";
            
            for (int i = 0; i < row.Tiles.Length; i++)
            {
                formedWord += row.Tiles[i].Letter.ToString();
                if (formedWord.Equals(solutionWord))
                {
                    return true;
                }
            }

            return false;
        }

        public void OnBackspaceInput()
        {
            Row currentRow = rows[rowIndex];
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            currentRow.Tiles[columnIndex].SetLetter('\0');
            currentRow.Tiles[columnIndex].SetTileState(EmptyState);
        }

        public void OnEnterClicked()
        {
            Row currentRow = rows[rowIndex];
            if (columnIndex >= currentRow.Tiles.Length)
            {
                SubmitRow(currentRow);
            }
        }

        private void OnDestroy()
        {
            AudioInstanceWin.release();
            AudioInstanceLose.release();
        }
    }
}
