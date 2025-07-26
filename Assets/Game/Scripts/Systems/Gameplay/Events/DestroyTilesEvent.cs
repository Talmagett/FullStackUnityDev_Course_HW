using UnityEngine;

public struct DestroyTilesEvent
{
    public Vector2Int[] TilePositions { get; }

    public DestroyTilesEvent(Vector2Int[] tilePositions)
    {
        TilePositions = tilePositions;
    }
}