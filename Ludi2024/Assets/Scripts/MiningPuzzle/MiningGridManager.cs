using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace MiningPuzzle
{
    public class MiningGridManager : MonoBehaviour, IGrid
    {
        public int width;
        public int height;
        public int numberOfItems;
        public int miningDepth;

        [Header("Prefabs")]
        public GameObject renderingOrigin;
        public List<MiningItem> miningItems;
        public List<GameObject> emptyMiningTilePrefabs;
        
        [Header("Camera")]
        public Camera mainCamera;


        private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();
        
        private void Awake()
        {
            GenerateGrid();
        }

        private void GenerateItems()
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                MiningItem item = miningItems[Random.Range(0, miningItems.Count)];
                Vector3 position = GetRandomPositionForItems(item);
                
                for (int x = 0; x < item.HorizontalSize; x++)
                {
                    for (int z = 0; z < item.VerticalSize; z++)
                    {
                        Vector3 itemPosition = new Vector3(position.x + x, position.y, position.z + z);
                        occupiedPositions.Add(itemPosition);
                        Instantiate(item.ItemPrefab, itemPosition, Quaternion.identity);
                    }
                }
            }
        }

        private Vector3 GetRandomPositionForItems(MiningItem item)
        {
            int x = Random.Range(1, width - item.HorizontalSize + 1);
            int z = Random.Range(1, height - item.VerticalSize + 1);
            return new Vector3(x * GetTileWidth(), miningDepth, z * GetTileHeight());
        }

        public void GenerateGrid()
        {
            for (int x = 1; x <= width; x++)
            {
                for (int z = 1; z <= height; z++)
                {
                    for (int y = 1; y <= miningDepth; y++)
                    {
                        Vector3 position = new Vector3(x * GetTileWidth(), y , z * GetTileHeight()) + renderingOrigin.transform.position;
                        if (occupiedPositions.Contains(new Vector3(x, y, z)))
                        {
                            continue;
                        }
                        GameObject tilePrefab = GetTileBasedOnDepth(y);
                        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                        Instantiate(tilePrefab, position, rotation);
                    }
                }
            }
        }

        private float GetTileWidth()
        {
            return emptyMiningTilePrefabs[0].GetComponent<Renderer>().bounds.size.x;
        }
        
        private float GetTileHeight()
        {
            return emptyMiningTilePrefabs[0].GetComponent<Renderer>().bounds.size.z;
        }

        private GameObject GetTileBasedOnDepth(int z)
        {
            return z switch
            {
                1 => emptyMiningTilePrefabs[0],
                2 => emptyMiningTilePrefabs[1],
                3 => emptyMiningTilePrefabs[2],
                4 => emptyMiningTilePrefabs[3],
                5 => emptyMiningTilePrefabs[4],
                _ => emptyMiningTilePrefabs[0]
            };
        }
    }
}
