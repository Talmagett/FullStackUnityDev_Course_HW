using System;
using System.Collections.Generic;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.Game.Items;
using Game.System.Gameplay.Quests;
using Modules.Animations;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.App
{
    public class LevelController : MonoBehaviour
    {
        /*
        [SerializeField] private Transform itemGrid;
        [SerializeField] private Transform pointGrid;
        [SerializeField] private Transform questTarget;
        
        [Inject] private IMap _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private ItemView _itemView;
        [Inject] private SoundPlayer _soundPlayer;
        [Inject] private Quest _quest;
        private LevelConfig _currentLevel;
        private Transform[,] _points;
        private ItemView[,] _grid;
        private Vector2Int GridSize;
        
        private void BuildLevel()
        {
            var items = _currentLevel.Field.items;
            
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

        public bool TrySwap(ItemView item, Vector2Int direction, out ItemView item2)
        {
            item2 = null;
            if (!_isInteractable) return false;
            
            Vector2Int gridPos = item.GridPosition;
            Vector2Int targetPos = gridPos + direction;

            if (!IsValidPosition(targetPos)) return false;
            item2 = GetItemAt(targetPos);
            if (item2 == null) throw new NullReferenceException("There is no item at target position");
            var item2GridPos = item2.GridPosition;
            item2.SetGridPosition(item.GridPosition);
            item.SetGridPosition(item2GridPos);
            
            (_grid[gridPos.x, gridPos.y], _grid[targetPos.x, targetPos.y]) = (_grid[targetPos.x, targetPos.y], _grid[gridPos.x, gridPos.y]);
            return true;
        }

        private void CheckTheField()
        {
            //if (!CheckForMatches())
            {
                if (_isGameOver)
                {
                    return;
                }

                _isInteractable = true;
                return;
            }

            _isInteractable = false;
            var queue = new AnimationQueue();
            //rework
            //queue.Enqueue(new ActionAnimation(RemoveMatches));
            queue.Enqueue(new DelayAnimation(0.2f));
            //queue.Enqueue(new ActionAnimation(FallDownItems));
            queue.Enqueue(new DelayAnimation(0.2f));
            //queue.Enqueue(new ActionAnimation(SpawnNewItems));
            queue.Execute();
        }
        
        private bool IsValidPosition(Vector2Int pos)
        {
            return pos.x >= 0 && pos.y >= 0 && pos.x < _grid.GetLength(0) && pos.y < _grid.GetLength(1);
        }
        
        public HashSet<ItemView> FindMatches()
        {
            var matchedItems = new HashSet<ItemView>();

            // Проверка по горизонтали
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1) - 2; y++)
                {
                    if (AreItemsMatching(_grid[x, y], _grid[x, y + 1], _grid[x, y + 2]))
                    {
                        matchedItems.Add(_grid[x, y]);
                        matchedItems.Add(_grid[x, y + 1]);
                        matchedItems.Add(_grid[x, y + 2]);
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
                        matchedItems.Add(_grid[x, y]);
                        matchedItems.Add(_grid[x + 1, y]);
                        matchedItems.Add(_grid[x + 2, y]);
                    }
                }
            }

            return matchedItems;
        }
        
        
        private bool AreItemsMatching(ItemView a, ItemView b, ItemView c)
        {
            return a != null && b != null && c != null && a.ItemType == b.ItemType && b.ItemType == c.ItemType;
        }
        
        public void RemoveMatches(HashSet<ItemView> matches)
        {
            foreach (var item in matches)
            {
                _grid[item.GridPosition.x,item.GridPosition.y] = null;
            }
        }
        
        public HashSet<ItemView> FallDownItems()
        {
            var fallingItems = new HashSet<ItemView>();

            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++) // Проходим снизу вверх
                {
                    if (_grid[x, y] != null) continue; // Пропускаем, если клетка занята
            
                    for (int dropY = y + 1; dropY < _grid.GetLength(1); dropY++) // Ищем сверху вниз
                    {
                        if (_grid[x, dropY] == null) continue;
                        
                        _grid[x, y] = _grid[x, dropY];
                        _grid[x, dropY] = null;
                        _grid[x, y].SetGridPosition(new Vector2Int(x, y));
                        fallingItems.Add(_grid[x, y]);
                        break;
                    }
                }
            }

            return fallingItems;
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

        public ItemView GetItemAt(Vector2Int itemGridPosition) => _grid[itemGridPosition.x, itemGridPosition.y];

        public void FillEmptySpaces()
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
        }*/
    }
}