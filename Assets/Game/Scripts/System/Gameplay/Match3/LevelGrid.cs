using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.System.Gameplay.Quests;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.System.Gameplay.Match3
{
    [UsedImplicitly]
    public class LevelGrid : ILevelGrid,IInitializable
    {
        [Inject] private IMap _map;
        
        private readonly LevelConfig _currentLevel;
        private Item[,] _grid;
        private Vector2Int _gridSize;
        
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
        
        public Item GetItem(Vector2Int pos) => _grid[pos.x, pos.y];
        public void SetItem(Vector2Int pos, Item item) => _grid[pos.x, pos.y] = item;
        
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
        }

        private void BuildLevel()
        {
            _grid = new Item[_gridSize.x+1,_gridSize.y+1];
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