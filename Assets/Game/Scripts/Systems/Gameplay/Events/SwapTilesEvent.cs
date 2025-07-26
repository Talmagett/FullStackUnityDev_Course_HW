using UnityEngine;

public struct SwapTilesEvent
{
    public Vector2Int Tile1Position { get; }
    public Vector2Int Tile2Position { get; }
    public SwapTilesEvent(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        Tile1Position = tile1Position;
        Tile2Position = tile2Position;
    }
}