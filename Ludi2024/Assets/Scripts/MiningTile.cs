using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTile : MonoBehaviour
{
    [SerializeField] private float _tileWidth;
    [SerializeField] private float _tileHeight;
    private MiningItem _item;

    public float TileWidth => _tileWidth;
    public float TileHeight => _tileHeight;

    [SerializeField] private MiningTileType _tileType;

    public void SetTileType(MiningTileType tileType)
    {
        _tileType = tileType;
    }
    
    public void SetMiningItem(MiningItem item)
    {
        _item = item;
    }
}

public enum MiningTileType
{
    Empty,
    Item
}
