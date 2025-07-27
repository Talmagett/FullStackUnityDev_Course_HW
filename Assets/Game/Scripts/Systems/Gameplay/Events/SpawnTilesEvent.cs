using Game.Gameplay.Items;
using UnityEngine;

public struct SpawnTilesEvent
{
    public Vector2Int[] TilePositions { get; }
    public ItemColor[] ItemTypes { get; }
    public SpawnTilesEvent(Vector2Int[] tilePositions, ItemColor[] itemTypes)
    {
        TilePositions = tilePositions;
        ItemTypes = itemTypes;
    }
}