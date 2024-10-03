using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTile : MonoBehaviour
{
    [SerializeField] private float _tileWidth;
    [SerializeField] private float _tileHeight;

    [SerializeField] private MiningTileType _tileType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum MiningTileType
{
    Empty,
    Item
}
