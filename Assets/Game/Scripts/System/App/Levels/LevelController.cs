using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;
using Modules.Inputs;
using UnityEngine;
using Zenject;

namespace Game.App
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform itemGrid;
        [SerializeField] private Transform pointGrid;
        
        [Inject] private IMap _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private ItemView _itemView;
        
        private LevelConfig _currentLevel;
        private Transform[,] _points;
        private ItemView[,] _grid;
        private Vector2Int GridSize;
        
        private bool _isInteractable=true;
        private void Awake()
        {
            _currentLevel = _map.CurrentLevel;
            BuildLevel();
        }

        private void BuildLevel()
        {
            var items = _currentLevel.Field.items;
            
            GridSize = new Vector2Int(0,0);
            foreach (var item in items)
            {
                if (item.point.x > GridSize.x)
                {
                    GridSize.x = item.point.x;
                }
                if (item.point.y > GridSize.y)
                {
                    GridSize.y = item.point.y;
                }
            }

            _points = new Transform[GridSize.x+1,GridSize.y+1];
            _grid = new ItemView[GridSize.x+1,GridSize.y+1];
            foreach (var item in items)
            {
                var point=new GameObject($"Point {item.point}");
                point.transform.SetParent(pointGrid);
                _points[item.point.x, item.point.y] = point.transform;

                var itemView = SpawnNewItem(item.point);
                itemView.SetSprite(_itemSpriteMap.GetItemSprite(item.type));
                itemView.SetItemType(item.type);
                _grid[item.point.x,item.point.y] = itemView;
            }

            pointGrid.position = -(Vector2)GridSize / 2;
            itemGrid.position = -(Vector2)GridSize / 2;
        }

        public void TrySwap(ItemView item, Vector2Int direction)
        {
            if (!_isInteractable) return;
            
            Vector2Int gridPos = item.GridPosition;
            Vector2Int targetPos = gridPos + direction;

            if (!IsValidPosition(targetPos)) return;
            
            var queue = new AnimationQueue();
            //(_grid[pos1.x, pos1.y], _grid[pos2.x, pos2.y]) = (_grid[pos2.x, pos2.y], _grid[pos1.x, pos1.y]);

            queue.Enqueue(new SwapItemsAnimation(_grid[gridPos.x, gridPos.y], _grid[targetPos.x, targetPos.y]));
            queue.Enqueue(new ActionAnimation(()=>SwapItems(gridPos,targetPos)));
            queue.Execute();
        }

        private void CheckTheField()
        {
            if (!CheckForMatches())
            {
                _isInteractable = true;
                return;
            }

            _isInteractable = false;
            var queue = new AnimationQueue();
            //queue.Enqueue(new ActionAnimation(DropDownItems));
            queue.Enqueue(new DelayAnimation(0.3f));
            queue.Enqueue(new ActionAnimation(RemoveMatches));
            queue.Enqueue(new DelayAnimation(0.3f));
            queue.Enqueue(new ActionAnimation(SpawnNewItems));
            queue.Execute();
        }
        private bool IsValidPosition(Vector2Int pos)
        {
            return pos.x >= 0 && pos.y >= 0 && pos.x < _grid.GetLength(0) && pos.y < _grid.GetLength(1);
        }

        private List<ItemView> _matchedItems;
        private bool CheckForMatches()
        {
            bool hasMatch = false;
            _matchedItems = new List<ItemView>();

            // Проверка по горизонтали
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1) - 2; y++)
                {
                    if (AreItemsMatching(_grid[x, y], _grid[x, y + 1], _grid[x, y + 2]))
                    {
                        _matchedItems.Add(_grid[x, y]);
                        _matchedItems.Add(_grid[x, y + 1]);
                        _matchedItems.Add(_grid[x, y + 2]);
                        hasMatch = true;
                    }
                }
            }

            // Проверка по вертикали
            for (int y = 0; y < _grid.GetLength(1); y++)
            {
                for (int x = 0; x < _grid.GetLength(0) - 2; x++)
                {
                    if (AreItemsMatching(_grid[x, y], _grid[x + 1, y], _grid[x + 2, y]))
                    {
                        _matchedItems.Add(_grid[x, y]);
                        _matchedItems.Add(_grid[x + 1, y]);
                        _matchedItems.Add(_grid[x + 2, y]);
                        hasMatch = true;
                    }
                }
            }

            return hasMatch;
        }
        private bool AreItemsMatching(ItemView a, ItemView b, ItemView c)
        {
            return a != null && b != null && c != null && a.ItemType == b.ItemType && b.ItemType == c.ItemType;
        }

        private void SwapItems(Vector2Int pos1, Vector2Int pos2)
        {
            (_grid[pos1.x, pos1.y], _grid[pos2.x, pos2.y]) = (_grid[pos2.x, pos2.y], _grid[pos1.x, pos1.y]);
            CheckTheField();
        }
        
        private void RemoveMatches()
        {
            foreach (var item in _matchedItems)
            {
                Destroy(item.gameObject);
            }
        }
        
        private void DropDownItems()
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = _grid.GetLength(1) - 1; y >= 0; y--)
                {
                    if (_grid[x, y] == null) // Если пусто
                    {
                        for (int dropY = y - 1; dropY >= 0; dropY--)
                        {
                            if (_grid[x, dropY] != null)
                            {
                                _grid[x, y] = _grid[x, dropY];
                                _grid[x, dropY] = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void SpawnNewItems()
        {
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    if (_grid[x, y] != null) continue;
                    
                    var newItem = SpawnNewItem(new Vector2Int(x, y));
                    _grid[x, y] = newItem;
                    var randomType = (ItemType)Random.Range(1, 7);
                    newItem.SetItemType(randomType);
                    newItem.SetSprite(_itemSpriteMap.GetItemSprite(randomType));
                }
            }

            CheckTheField();
        }

        private ItemView SpawnNewItem(Vector2Int position)
        {
            var newItem = Instantiate(_itemView, itemGrid);
            newItem.transform.localPosition=(Vector2)(position);
            newItem.SetGridPosition(position);
            return newItem;
        }
    }
}