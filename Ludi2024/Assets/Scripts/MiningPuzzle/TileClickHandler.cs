using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiningPuzzle
{
    public class TileClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public MiningTileType TileType { get; set; }
        public MiningItem Item { get; set; }

        private MiningGridManager gridManager;
        private Vector3Int gridPosition;

        private void Awake()
        {
            gridManager = MiningGridManager.Instance;
        }

        public void Initialize(Vector3Int position)
        {
            gridPosition = position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (TileType == MiningTileType.Item)
            {
                // Perform action for item tile
            }
            else if (TileType == MiningTileType.Empty)
            {
                // Perform action for empty tile
                gridManager.SetGridPositionToAir(gridPosition);
                if (gridManager.CheckIfAllItemsAreDiscovered())
                {
                    Debug.LogWarning("All items discovered!");
                }
                Destroy(gameObject);
            }
        }
    }
}