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


        public void GenerateGrid()
        {
            
        }
    }
}
