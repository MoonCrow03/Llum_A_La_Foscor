using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace MiningPuzzle
{
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
        
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;

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
            PositionCameraAboveGrid(mainCamera);
        }
    
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
                            InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefab, MiningTileType.Empty);

                        }
                        else if (TileBelowExists(new Vector3Int(x, y, z)))
                        {
                            float probability = 1 - (float) y / miningDepth;
                            if (Random.value < probability)
                            {
                                occupiedPositions[x, y, z] = true;
                                grid [x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Empty);
                                InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefab, MiningTileType.Empty);
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
            
                if (position.x + size[0] < width && position.z + size[1] < height && !occupiedPositions[position.x, position.y - 1, position.z])
                {
                    for (int x = position.x; x < position.x + size[0]; x++)
                    {
                        for (int z = position.z; z < position.z + size[1]; z++)
                        {
                            occupiedPositions[x, position.y - 1, z] = true;
                            grid[x, position.y, z] = new MiningTile(new Vector3Int(x, position.y-1, z), MiningTileType.Item)
                            { 
                                Item = miningItem
                            };
                            InstantiateTileGameObject(new Vector3Int(x, position.y-1, z), miningItem.ItemPrefab, MiningTileType.Item, miningItem);
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
    
        private void InstantiateTileGameObject(Vector3Int position, GameObject prefab, MiningTileType tileType, MiningItem item = null)
        {
            GameObject tileObject = Instantiate(prefab, position, Quaternion.identity);
            tileObject.transform.SetParent(renderingGrid.transform);

            TileClickHandler clickHandler = tileObject.GetComponent<TileClickHandler>();
            if (clickHandler != null)
            {
                clickHandler.TileType = tileType;
                clickHandler.Item = item;
            }
        }
        Vector3Int GetRandomPosition()
        {
            int x = Random.Range(0, width);
            int y = 1;
            int z = Random.Range(0, height);

            return new Vector3Int(x, y, z);
        }

        void PositionCameraAboveGrid(Camera camera)
        {
            Vector3 centerPosition = new Vector3(width / 2f, miningDepth + 10f, height / 2f);
            camera.transform.position = centerPosition;
            camera.transform.LookAt(new Vector3(width / 2f, 0f, height / 2f));
        }

    }
}
