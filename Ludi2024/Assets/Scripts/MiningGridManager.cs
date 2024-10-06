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

    [FormerlySerializedAs("_renderingGrid")]
    [Header("Prefabs")]
    [SerializeField] private GameObject renderingGrid;
    [SerializeField] private MiningTile emptyMiningTilePrefab;
    [SerializeField] private List<MiningItem> miningItems;

    private GameObject[,,] grid;
    private bool[,,] occupiedPositions;

    void Awake()
    {
        grid = new GameObject[width, miningDepth, height];
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
                    occupiedPositions[x, y, z] = true;
                    MiningTile miningTile = Instantiate(emptyMiningTilePrefab, new Vector3Int(x, y, z), Quaternion.identity);
                    miningTile.transform.SetParent(renderingGrid.transform);
                    miningTile.transform.localScale = new Vector3Int(1, 1, 1);
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
                            Debug.Log("Generating item at: " + x + ", " + position.y + ", " + z);
                            GameObject itemObject = Instantiate(miningItem.ItemPrefab, new Vector3Int(x, position.y, z),
                                Quaternion.identity);
                            itemObject.transform.localScale = new Vector3Int(1, 1, 1);
                            itemObject.transform.SetParent(renderingGrid.transform);
                            occupiedPositions[x, position.y-1, z] = true;
                            grid[x, position.y, z] = itemObject;
                            
                        }
                    }
                }
                itemsPlaced++;
            }
        }
    }
    
    bool IsPositionOccupied(Vector3Int position, int[] size)
    {
        for (int x = position.x; x < position.x + size[0]; x++)
        {
            for (int z = position.z; z < position.z + size[1]; z++)
            {
                if (grid[x, position.y, z] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    Vector3Int GetRandomPosition()
    {
        int x = Random.Range(0, width);
        int y = 1;
        int z = Random.Range(0, height);

        return new Vector3Int(x, y, z);
    }


}
