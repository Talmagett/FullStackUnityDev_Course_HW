using System.Collections.Generic;
using System.Text;
using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.System.App.Map;
using Game.System.Gameplay.Quests;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.System.Gameplay.Match3
{
    [UsedImplicitly]
    public class LevelGrid : ILevelGrid, IInitializable
    {
        public Vector2Int GridSize => _gridSize;
        
        [Inject] private IMap _map;
        
        private readonly LevelConfig _currentLevel;
        private Item[,] _grid;
        private Vector2Int _gridSize;
        
        public IEnumerable<Item> GetAllItems()
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    yield return _grid[x, y];
                }
            }
        }

        public LevelGrid(IMap map, Quest quest)
        {
            _map = map;
            _currentLevel = _map.CurrentLevel;
        }

        public void Initialize()
        {
            CalculateGridSize();
            BuildLevel();
        }

        public Item GetItem(Vector2Int pos)
        {
            if (pos.x < 0 || pos.x >= _gridSize.x || pos.y < 0 || pos.y >= _gridSize.y)
            {
                return null;
            }
            return _grid[pos.x, pos.y];
        }

        public void SetItem(Vector2Int pos, Item item)
        {
            _grid[pos.x, pos.y] = item;
            item.SetGridPosition(pos);
        }

        private void CalculateGridSize()
        {
            var items = _currentLevel.Field.items;
            
            _gridSize = new Vector2Int(0,0);
            foreach (var item in items)
            {
                if (item.point.x > _gridSize.x)
                {
                    _gridSize.x = item.point.x;
                }
                if (item.point.y > _gridSize.y)
                {
                    _gridSize.y = item.point.y;
                }
            }
            _gridSize += Vector2Int.one;
        }

        private void BuildLevel()
        {
            _grid = new Item[_gridSize.x,_gridSize.y];
            foreach (var itemData in _currentLevel.Field.items)
            {
                CreateItem(itemData.type, itemData.point);
            }
        }
        
        public void FillEmptySpaces()
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    if (_grid[x, y] != null) continue;
                    
                    var randomType = (ItemType)Random.Range(1, 7);
                    CreateItem(randomType, new Vector2Int(x, y));
                }
            }
        }

        private void CreateItem(ItemType type, Vector2Int pos)
        {
            var item = new Item(type);
            item.SetGridPosition(pos);
            _grid[pos.x, pos.y] = item;
        }
    }

    public interface ILevelGrid
    {
        Item GetItem(Vector2Int pos);
        void SetItem(Vector2Int pos, Item item);
    }
}