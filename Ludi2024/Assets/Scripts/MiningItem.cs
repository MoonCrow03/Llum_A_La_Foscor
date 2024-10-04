using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiningItem", menuName = "MiningItem")]
public class MiningItem : ScriptableObject
{
    [SerializeField] private int _horizontalTilesSize;
    [SerializeField] private int _verticalTilesSize;
    public int HorizontalTilesSize => HorizontalTilesSize;
    public int VerticalTilesSize => VerticalTilesSize;
}
