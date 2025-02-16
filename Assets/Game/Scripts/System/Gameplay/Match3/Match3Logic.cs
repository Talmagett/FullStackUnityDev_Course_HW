using System.Collections.Generic;
using System.Linq;
using Game.Common;
using Game.System.Gameplay.Quests;
using UnityEngine;

namespace Game.System.Gameplay.Match3
{
    public class Match3Logic
    {
        private readonly LevelGrid _grid;
        private readonly Quest _questTracker;

        public Match3Logic(LevelGrid grid, Quest questTracker)
        {
            _grid = grid;
            _questTracker = questTracker;
        }

        public bool TrySwap(Vector2Int pos1, Vector2Int pos2)
        {
            var item1 = _grid.GetItem(pos1);
            var item2 = _grid.GetItem(pos2);
            if (item1 == null || item2 == null) return false;

            _grid.MoveItem(pos1, item2);
            _grid.MoveItem(pos2, item1);
            
            return true;
        }
        
        public HashSet<Item> FindMatches()
        {
            var matchedItems = new HashSet<Item>();

            // Проверка по горизонтали
            for (int x = 0; x < _grid.GridSize.x; x++)
            {
                for (int y = 0; y < _grid.GridSize.y - 2; y++)
                {
                    var item1 = _grid.GetItem(new Vector2Int(x, y));
                    var item2 = _grid.GetItem(new Vector2Int(x, y + 1));
                    var item3 = _grid.GetItem(new Vector2Int(x, y + 2));
                    if (AreItemsMatching(item1, item2,item3))
                    {
                        matchedItems.Add(item1);
                        matchedItems.Add(item2);
                        matchedItems.Add(item3);
                    }
                }
            }

            // Проверка по вертикали
            for (int y = 0; y < _grid.GridSize.x; y++)
            {
                for (int x = 0; x < _grid.GridSize.y - 2; x++)
                {
                    var item1 = _grid.GetItem(new Vector2Int(x, y));
                    var item2 = _grid.GetItem(new Vector2Int(x + 1, y));
                    var item3 = _grid.GetItem(new Vector2Int(x + 2, y));
                    if (AreItemsMatching(item1, item2, item3))
                    {
                        matchedItems.Add(item1);
                        matchedItems.Add(item2);
                        matchedItems.Add(item3);
                    }
                }
            }

            return matchedItems;
        }
        
        private bool AreItemsMatching(Item a, Item b, Item c)
        {
            return a != null && b != null && c != null && a.ItemType == b.ItemType && b.ItemType == c.ItemType;
        }
        
        public void RemoveMatches(IEnumerable<Item> matchedItems)
        {
            foreach (var item in matchedItems)
            {
                _grid.RemoveItem(item.GridPosition);
                //_questTracker.CheckItem(item); // Отслеживаем квестовые фишки
            }
        }

        public HashSet<FallingData> FallDownItems()
        {
            var fallingItems = new HashSet<FallingData>();

            for (int x = 0; x < _grid.GridSize.x; x++)
            {
                for (int y = 0; y < _grid.GridSize.y; y++)
                {
                    var pos=new Vector2Int(x, y);
                    var item = _grid.GetItem(pos);
                    if (item!=null) continue;
                    
                    for (int dropY = y + 1; dropY < _grid.GridSize.y; dropY++)
                    {
                        var itemPositionAbove = new Vector2Int(pos.x, pos.y + dropY);
                        var itemAbove = _grid.GetItem(itemPositionAbove);
                        if (itemAbove==null) continue;
                        _grid.MoveItem(pos,itemAbove);
                        fallingItems.Add(new FallingData(itemPositionAbove,pos,itemAbove));
                        break;
                    }
                }
            }

            return fallingItems;
        }

        public struct FallingData
        {
            public FallingData(Vector2Int previousPosition, Vector2Int nextPosition, Item item)
            {
                this.PreviousPosition = previousPosition;
                this.NextPosition = nextPosition;
                this.Item = item;
            }

            public Vector2Int PreviousPosition { get; }
            public Vector2Int NextPosition { get; }
            public Item Item { get; }
        }
    }
}