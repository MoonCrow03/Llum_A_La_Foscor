using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MiningGridManager : MonoBehaviour, IGrid
{
    [Header("Grid Settings")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _numberOfItems;
    [SerializeField] private int _miningDepth;

    [Header("Prefabs")]
    [SerializeField] private GameObject _renderingGrid;
    [SerializeField] private MiningTile _emptyMiningTilePrefab;
    [SerializeField] private List<MiningItem> _miningItems;

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < _miningDepth; i++)
        {
            Debug.Log("Generating Layer " + _miningDepth);
            GenerateLayer(i);
        }
    }

    void GenerateLayer(int miningDepth)
    {
        Vector3 gridOrigin = _renderingGrid.transform.position;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Vector3 tilePosition = new Vector3(gridOrigin.x + i, gridOrigin.y - miningDepth, gridOrigin.z - j);
                var generatedMiningTile = Instantiate(_emptyMiningTilePrefab, tilePosition, Quaternion.identity);
                generatedMiningTile.transform.SetParent(_renderingGrid.transform);
            }
        }
    }

    void PlaceItemsOnGrid()
    {

    }


}
