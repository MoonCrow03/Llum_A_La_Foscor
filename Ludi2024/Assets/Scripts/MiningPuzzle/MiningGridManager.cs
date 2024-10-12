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


        private void Awake()
        {
            GenerateGrid();
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
