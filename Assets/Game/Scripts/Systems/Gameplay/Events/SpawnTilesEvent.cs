using Game.Gameplay.Items;
using UnityEngine;

public struct SpawnTilesEvent
{
    public Vector2Int[] TilePositions { get; }
    public ItemType[] ItemTypes { get; }
    public SpawnTilesEvent(Vector2Int[] tilePositions, ItemType[] itemTypes)
    {
        TilePositions = tilePositions;
        ItemTypes = itemTypes;
    }
}