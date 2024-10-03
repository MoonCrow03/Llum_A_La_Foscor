using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts;
using UnityEngine;

public class MiningGridManager : MonoBehaviour, IGrid
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _numberOfItems;
    [SerializeField] private int _miningDepth;

    [SerializeField] private GameObject _renderingGrid;
    [SerializeField] private MiningTile _emptyMiningTilePrefab;
    [SerializeField] private List<MiningItem> _miningItems;

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < _miningDepth; i++)
        {
            Debug.Log("Generating Layer " + _miningDepth);
            GenerateLayer(_miningDepth);
        }
    }

    void GenerateLayer(int miningDepth)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var generatedMiningTile = Instantiate(_emptyMiningTilePrefab, new Vector3(i, j, miningDepth), Quaternion.identity);
            }
        }
    }

    void PlaceItemsOnGrid()
    {

    }


}
