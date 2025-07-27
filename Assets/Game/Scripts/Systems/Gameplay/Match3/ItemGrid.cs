using System;
using System.Collections.Generic;
using Game.App.Levels;

using Game.Gameplay.Items;
using Game.Gameplay.Quests;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Match3
{
    [UsedImplicitly]
    public class ItemGrid : Grid<ItemColor>, IItemGrid, IInitializable
    {
        public event Action<IEnumerable<Item>> OnGridChanged;
        public Vector2Int GridSize { get; private set; }
        
        [Inject] private ILevelService _map;
        
        private readonly LevelConfig _currentLevel;
        private readonly Dictionary<Vector2Int, Item> _gridItems = new();
        
        public ItemGrid(ILevelService map, Quest quest): base(0, 0)
        {
            _map = map;
            _currentLevel = _map.CurrentLevel;
        }
        
        public IEnumerable<Item> GetAllItems()
        {
            return _gridItems.Values;
        }

        public void Initialize()
        {
            CalculateGridSize();
            BuildLevel();
        }

        public Item GetItem(Vector2Int pos)
        {
            if (!_gridItems.ContainsKey(pos))
                return null;
            return _gridItems.TryGetValue(pos, out Item item) ? item : null;
        }

        public void MoveItem(Vector2Int pos, Item item)
        {
            if (!_gridItems.ContainsKey(pos)) return;
            _gridItems[pos] = item;
            item.SetGridPosition(pos);
        }

        public void RemoveItem(Vector2Int pos)
        {
            if (!_gridItems.ContainsKey(pos)) return;
            _gridItems[pos] = null;
        }
        
        private void CalculateGridSize()
        {
            var items = _currentLevel.Field.items;
            
            var gridSize = new Vector2Int(0,0);
            foreach (var item in items)
            {
                if (item.point.x > gridSize.x)
                {
                    gridSize.x = item.point.x;
                }
                if (item.point.y > gridSize.y)
                {
                    gridSize.y = item.point.y;
                }
            }
            gridSize += Vector2Int.one;
            GridSize = gridSize;
        }

        private void BuildLevel()
        {
            foreach (var itemData in _currentLevel.Field.items)
            {
                CreateItem(itemData.type, itemData.point);
            }

            OnGridChanged?.Invoke(_gridItems.Values);
        }
        
        public List<Item> FillEmptySpaces()
        {
            var newItems = new List<Item>();
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    var pos = new Vector2Int(x, y);
                    if (!_gridItems.ContainsKey(pos)) continue;
                    if (_gridItems[pos] != null) continue;
                    
                    var randomType = (ItemColor)Random.Range(1, 7);
                    var newItem = CreateItem(randomType, new Vector2Int(x, y));
                    newItems.Add(newItem);
                }
            }
            OnGridChanged?.Invoke(newItems);
            return newItems;
        }

        private Item CreateItem(ItemColor type, Vector2Int pos)
        {
            var item = new Item(type);
            _gridItems[pos] = item;
            item.SetGridPosition(pos);
            return item;
        }
    }

    public interface IItemGrid
    {
        Item GetItem(Vector2Int pos);
        void MoveItem(Vector2Int pos, Item item);
    }
}