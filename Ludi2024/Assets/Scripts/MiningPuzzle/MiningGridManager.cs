using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

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
        [SerializeField] private List<MiningItem> miningItems;
        [SerializeField] private List<GameObject> emptyMiningTilePrefabs;
        
        [Header("Camera")]
        [SerializeField] private Camera mainCamera;

        

        private MiningTile[,,] grid;
        private bool[,,] occupiedPositions;
        
        public static MiningGridManager Instance;

        void Awake()
        {
            grid = new MiningTile[width, miningDepth, height];
            occupiedPositions = new bool[width, miningDepth, height];
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple instances of MiningGridManager found. Destroying this instance.");
                Destroy(gameObject);
            }
        }

        void Start()
        {
            PlaceItemsOnGrid();
            GenerateGrid();
            PositionCameraAboveGrid(mainCamera);
        }

        private void Update()
        {
            /*if (CheckIfAllItemsAreDiscovered())
            {
                Debug.LogWarning("All items discovered!");
            }*/

        }

        private int countCellsInitems()
        {
            int totalCells = 0;
            foreach (var miningItem in miningItems)
            {
                totalCells += miningItem.HorizontalTilesSize * miningItem.VerticalTilesSize;
            }

            return totalCells;
        }

        public bool CheckIfAllItemsAreDiscovered()
        {
            int totalCountedCells = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < miningDepth; y++)
                {
                    for (int z = 0; z < height; z++)
                    {
                        /*if (grid[x, y, z] == null)
                        {
                            continue;
                        }*/
                        if (grid[x, y, z].TileType == MiningTileType.Item)
                        { 
                            totalCountedCells++;
                            for (int aboveY = y + 1; aboveY < miningDepth; aboveY++)
                            {
                                if (grid[x, aboveY, z].TileType == MiningTileType.Empty)
                                {
                                    totalCountedCells--;
                                }
                            }
                        }
                    }
                }
            }
            if (totalCountedCells == countCellsInitems())
            {
                return true;
            }

            return false;
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
                        
                        if (occupiedPositions[x, y, z])
                        {
                            continue;
                        }
                        if (y == 0 || y == 1)
                        {
                            occupiedPositions[x, y, z] = true;
                            grid[x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Empty);
                            switch (y)
                            {
                                case 0:
                                    InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefabs[0], MiningTileType.Empty);
                                    break;
                                case 1:
                                    InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefabs[1], MiningTileType.Empty);
                                    break;
                            }
                        }
                        else if (TileBelowExists(new Vector3Int(x, y, z)))
                        {
                            float probability = 1 - (float) y / miningDepth;
                            if (Random.value < probability)
                            {
                                occupiedPositions[x, y, z] = true;
                                grid [x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Empty);
                                switch (y)
                                {
                                    case 3:
                                        InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefabs[2], MiningTileType.Empty);
                                        break;
                                    case 4:
                                        InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefabs[3], MiningTileType.Empty);
                                        break;
                                    case 5:
                                        InstantiateTileGameObject(new Vector3Int(x, y, z), emptyMiningTilePrefabs[4], MiningTileType.Empty);
                                        break;
                                }
                            }
                        }
                        
                        if (grid[x, y, z] == null)
                        {
                            occupiedPositions[x, y, z] = true;
                            grid[x, y, z] = new MiningTile(new Vector3Int(x, y, z), MiningTileType.Air);
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
            
                if (!occupiedPositions[position.x, position.y-1, position.z] && position.x + size[0] < width && position.z + size[1] < height)
                {
                    for (int x = position.x; x < position.x + size[0]; x++)
                    {
                        for (int z = position.z; z < position.z + size[1]; z++)
                        {
                            occupiedPositions[x, position.y - 1, z] = true;
                            grid[x, position.y-1, z] = new MiningTile(new Vector3Int(x, position.y-1, z), MiningTileType.Item) { 
                                Item = miningItem
                            };
                            InstantiateTileGameObject(new Vector3Int(x, position.y-1, z), miningItem.ItemPrefab, MiningTileType.Item, miningItem);
                        }
                    }
                    itemsPlaced++;
                }
            }
        }
        
        public void SetGridPositionToAir(Vector3Int position)
        {
            occupiedPositions[position.x, position.y, position.z] = false;
            grid[position.x, position.y, position.z] = new MiningTile(position, MiningTileType.Air);
        }
    
        bool TileBelowExists(Vector3Int position)
        {
            return occupiedPositions[position.x, position.y - 1, position.z] && grid[position.x, position.y - 1, position.z].TileType != MiningTileType.Air;
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
                clickHandler.Initialize(position);
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
