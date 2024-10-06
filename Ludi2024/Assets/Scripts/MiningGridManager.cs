using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;

public class MiningGridManager : MonoBehaviour, IGrid
{
    [Header("Grid Settings")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int numberOfItems;
    [SerializeField] private int miningDepth;

    [Header("Prefabs")]
    [SerializeField] private GameObject renderingGrid;
    [SerializeField] private GameObject emptyMiningTilePrefab;
    [SerializeField] private List<MiningItem> miningItems;

    private MiningTile[,,] grid;
    private bool[,,] occupiedPositions;

    void Awake()
    {
        grid = new MiningTile[width, miningDepth, height];
        occupiedPositions = new bool[width, miningDepth, height];
    }

    void Start()
    {
        PlaceItemsOnGrid();
        GenerateGrid();
    }

    // TODO: Hacer que la profundidad cambie, no que en toda la grid tenga la misma profundidad

    public void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < miningDepth; y++)
            {
                for (int z = 0; z < height; z++)
                {
                    Debug.Log("Generating tile at: " + x + ", " + y + ", " + z);
                    if (occupiedPositions[x, y, z]) continue;

                    if (y == 0 || y == 1)
                    {
                        occupiedPositions[x, y, z] = true;
                        grid[x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Empty);
                        InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefab);
                    }
                    else if (TileBelowExists(new Vector3Int(x, y, z)))
                    {
                        float probability = 1 - (float) y / miningDepth;
                        if (Random.value < probability)
                        {
                            occupiedPositions[x, y, z] = true;
                            grid [x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Empty);
                            InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefab);
                        }
                    }
                    
                }
            }
        }
    }
    void PlaceItemsOnGrid()
    {
        int itemsPlaced = 0;
        while (itemsPlaced < numberOfItems)
        {
            MiningItem miningItem = miningItems[Random.Range(0, miningItems.Count)];
            Vector3Int position = GetRandomPosition();
            int[] size = miningItem.GetSize();
            
            if (position.x + size[0] < width && position.z + size[1] < height)
            {
                for (int x = position.x; x < position.x + size[0]; x++)
                {
                    for (int z = position.z; z < position.z + size[1]; z++)
                    {
                        if (!occupiedPositions[x, position.y-1, z])
                        {
                            occupiedPositions[x, position.y - 1, z] = true;
                            grid[x, position.y, z] = new MiningTile(new Vector3Int(x, position.y-1, z), MiningTileType.Item)
                            {
                                Item = miningItem
                            };
                            InstantiateTileGameObject(new Vector3Int(x, position.y-1, z), miningItem.ItemPrefab);
                        }
                    }
                }
                itemsPlaced++;
            }
        }
    }
    
    bool TileBelowExists(Vector3Int position)
    {
        return occupiedPositions[position.x, position.y - 1, position.z];
    }
    
    void InstantiateTileGameObject(Vector3Int position, GameObject miningTilePrefab)
    {
        GameObject tile = Instantiate(miningTilePrefab, position, Quaternion.identity);
        tile.transform.SetParent(renderingGrid.transform);
    }
    Vector3Int GetRandomPosition()
    {
        int x = Random.Range(0, width);
        int y = 1;
        int z = Random.Range(0, height);

        return new Vector3Int(x, y, z);
    }
    
    


}
