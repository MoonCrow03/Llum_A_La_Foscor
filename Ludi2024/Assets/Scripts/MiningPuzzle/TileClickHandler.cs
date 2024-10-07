using UnityEngine;
using UnityEngine.EventSystems;

namespace MiningPuzzle
{
    public class TileClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public MiningTileType TileType { get; set; }
        public MiningItem Item { get; set; }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (TileType == MiningTileType.Item)
            {
                // Perform action for item tile
                Debug.Log("Item clicked: " + Item.name);
            }
            else if (TileType == MiningTileType.Empty)
            {
                // Perform action for empty tile
                Debug.Log("Empty tile clicked");
                Destroy(gameObject);
            }
        }
    }
}