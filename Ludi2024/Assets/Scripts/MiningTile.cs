using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTile
{
    public Vector3Int Position { get;}
    public MiningTileType TileType { get; set; }
    public MiningItem Item { get; set; }
    
    public MiningTile(Vector3Int position, MiningTileType tileType)
    {
        Position = position;
        TileType = tileType;
    }
}

public enum MiningTileType
{
    Empty,
    Item,
    Bomb
}
