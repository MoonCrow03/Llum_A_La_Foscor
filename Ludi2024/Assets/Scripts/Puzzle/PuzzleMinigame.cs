using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleMinigame : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [SerializeField] private int m_PuzzleSize;
    [SerializeField] private int m_PieceCounter;

    private void Start()
    {
        SetPlayablePieces();

        m_PieceCounter = 0;
    }

    private void SetPlayablePieces()
    {
        List<PuzzlePiece> l_puzzlePieces = new List<PuzzlePiece>(gameObject.GetComponentsInChildren<PuzzlePiece>());

        foreach (var t_puzzlePiece in l_puzzlePieces)
        {
            if (t_puzzlePiece.IsSimple())
            {
                m_PuzzleSize++;
            }
        }
    }

    private void EndGame()
    {
        if (m_PieceCounter == m_PuzzleSize)
        {
            Debug.Log("Puzzle completed!");
        }
    }

    private void RegisterCorrectPiece()
    {
        m_PieceCounter++;

        EndGame();
    }

    private void OnEnable()
    {
        PuzzlePiece.OnPiecePlaced += RegisterCorrectPiece;
    }

    private void OnDisable()
    {
        PuzzlePiece.OnPiecePlaced -= RegisterCorrectPiece;
    }
}
