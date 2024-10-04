using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using Assets.Scripts;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

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
        PlaceItemsOnGrid();
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < _miningDepth; i++)
        {
            Debug.Log("Generating Layer " + _miningDepth);
            GenerateLayer(i);
        }
    }

    // TODO: Hacer que la profundidad cambie, no que en toda la grid tenga la misma profundidad
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
        Debug.Log("Starting to place items");
        int deepestLayer = _miningDepth - 1;
        Vector3 gridOrigin = _renderingGrid.transform.position;
        HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

        for (int i = 0; i < _numberOfItems; i++)
        {
            MiningItem randomMiningItem = _miningItems[Random.Range(0, _miningItems.Count)];
            int itemWidth = randomMiningItem.HorizontalTilesSize;
            int itemHeight = randomMiningItem.VerticalTilesSize;

            bool itemPlaced = false;
            while (!itemPlaced)
            {
                int x = Random.Range(0, _width - itemWidth + 1);
                int y = Random.Range(0, _height - itemHeight + 1);
                Vector2 startPosition = new Vector2(x, y);

                // Check if the item can be placed at the startPosition
                bool canPlace = true;
                for (int dx = 0; dx < itemWidth && canPlace; dx++)
                {
                    for (int dy = 0; dy < itemHeight && canPlace; dy++)
                    {
                        Vector2 checkPosition = new Vector2(startPosition.x + dx, startPosition.y + dy);
                        if (occupiedPositions.Contains(checkPosition))
                        {
                            canPlace = false;
                        }
                    }
                }

                if (canPlace)
                {
                    // Mark the positions as occupied and place the item tiles
                    for (int dx = 0; dx < itemWidth; dx++)
                    {
                        for (int dy = 0; dy < itemHeight; dy++)
                        {
                            Vector2 occupiedPosition = new Vector2(startPosition.x + dx, startPosition.y + dy);
                            occupiedPositions.Add(occupiedPosition);

                            Vector3 itemTilePosition = new Vector3(gridOrigin.x + occupiedPosition.x, gridOrigin.y - deepestLayer, gridOrigin.z - occupiedPosition.y);
                            Debug.Log("Placing item tile at " + itemTilePosition);
                            Instantiate(randomMiningItem.ItemPrefab, itemTilePosition, Quaternion.identity);
                        }
                    }
                    itemPlaced = true;
                }
            }
        }
    }
}
