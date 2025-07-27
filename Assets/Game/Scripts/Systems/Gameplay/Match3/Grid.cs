using UnityEngine;
using System;
using System.Collections.Generic;

public class Grid<T>
{
    private readonly T[,] _data;

    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        _data = new T[width, height];
    }

    public bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < Width && pos.y >= 0 && pos.y < Height;
    }

    public T Get(Vector2Int pos)
    {
        if (!IsInside(pos))
            throw new IndexOutOfRangeException($"Invalid position: {pos}");
        return _data[pos.x, pos.y];
    }

    public void Set(Vector2Int pos, T value)
    {
        if (!IsInside(pos))
            throw new IndexOutOfRangeException($"Invalid position: {pos}");
        _data[pos.x, pos.y] = value;
    }

    public T this[int x, int y]
    {
        get => Get(new Vector2Int(x, y));
        set => Set(new Vector2Int(x, y), value);
    }

    public T this[Vector2Int pos]
    {
        get => Get(pos);
        set => Set(pos, value);
    }

    public IEnumerable<Vector2Int> AllPositions()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                yield return new Vector2Int(x, y);
    }
}
