using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiningItem", menuName = "MiningItem")]
public class MiningItem : ScriptableObject
{
    [SerializeField] private int _horizontalTilesSize;
    [SerializeField] private int _verticalTilesSize;
    [SerializeField] private GameObject _itemPrefab;

    public int HorizontalTilesSize => _horizontalTilesSize;
    public int VerticalTilesSize => _verticalTilesSize;
    public GameObject ItemPrefab => _itemPrefab;
}
