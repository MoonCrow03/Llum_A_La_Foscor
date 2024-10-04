using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PuzzleGridManager : MonoBehaviour, IGrid
{
    [Header("Grid Settings")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _pieces;

    [Header("Prefabs")]
    [SerializeField] private GameObject _renderingGrid;
    [SerializeField] private PuzzlePiece _piecePrefab;

    private List<Vector2Int> _indexes;

    private void Start()
    {
        _indexes = new List<Vector2Int>();
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        Vector3 gridOrigin = _renderingGrid.transform.position;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 tilePosition = new Vector3(gridOrigin.x + x, gridOrigin.y, gridOrigin.z - y);

                var tile = Instantiate(_piecePrefab, tilePosition, Quaternion.identity);
                tile.transform.SetParent(_renderingGrid.transform);
                tile.name = $"Tile ({x},{y})";

                _indexes.Add(new Vector2Int(x, y));
            }
        }
    }
}
