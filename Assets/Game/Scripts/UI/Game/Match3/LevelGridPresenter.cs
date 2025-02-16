using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Common;
using Game.System.Gameplay.Items;
using Game.System.Gameplay.Match3;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Match3
{
    public class LevelGridPresenter : IInitializable, IDisposable
    {
        private readonly LevelGrid _levelGrid;
        private readonly Match3Logic _match3Logic;
        private readonly LevelGridView _view;
        private readonly ItemSpriteMap _itemSpriteMap;
        private readonly ItemRepository _itemRepository;
        private readonly ItemInputHandler _itemInputHandler;
        private readonly AnimationQueue _animationQueue;

        public LevelGridPresenter(LevelGrid levelGrid, Match3Logic match3Logic, LevelGridView view, ItemSpriteMap itemSpriteMap, ItemInputHandler itemInputHandler)
        {
            _itemRepository = new ItemRepository();
            _animationQueue = new AnimationQueue();
            
            _levelGrid = levelGrid;
            _match3Logic = match3Logic;
            _view = view;
            _itemSpriteMap = itemSpriteMap;
            _itemInputHandler = itemInputHandler;
        }

        public void Initialize()
        {
            _itemInputHandler.OnItemSwipe+= HandleSwipe;
            _view.SetGridSize(_levelGrid.GridSize);
            _levelGrid.OnGridChanged += SpawnItems;
            SpawnItems(_levelGrid.GetAllItems());
        }

        public void Dispose()
        {
            _itemInputHandler.OnItemSwipe-= HandleSwipe;
            _levelGrid.OnGridChanged -= SpawnItems;
        }

        private void SpawnItems(IEnumerable<Item> enumerable)
        {
            foreach (var item in enumerable)
            {
                SpawnItem(item);
            }
        }

        private void SpawnItem(Item item)
        {
            var itemSprite = _itemSpriteMap.GetItemSprite(item.ItemType);
            var itemView = _view.SpawnItem(item.GridPosition, itemSprite);
            var itemPresenter = new ItemPresenter(item, itemView,_view.PositionOffset);
            _itemRepository.Add(item.GridPosition,itemPresenter);
        }
        
        private void HandleSwipe(ItemView itemView, Vector2Int direction)
        {
            HandleSwipeAsync(itemView, direction).Forget();
        }
        
         private async UniTask HandleSwipeAsync(ItemView itemView, Vector2Int direction)
         {
             if (_animationQueue.IsRunning) return;
             //if (_quest.IsQuestComplete()) return;

             var item1 = _itemRepository.GetItem(itemView);
             var item2 = _itemRepository.GetItem(item1.GridPosition + direction);
             if (item2 == null) return;
             
            SwapItems(item1, item2).Forget();
         }

         private async UniTask SwapItems(Item item1, Item item2)
         {
             if (!_match3Logic.TrySwap(item1.GridPosition, item2.GridPosition))
                 return;

             var itemView1 = _itemRepository.GetView(item1.GridPosition);
             var itemView2 = _itemRepository.GetView(item2.GridPosition);

             var swapAnimation = new SwapAnimation(itemView1, itemView2);
             _animationQueue.Enqueue(swapAnimation);
             await _animationQueue.Execute();

             _itemRepository.Swap(item1.GridPosition, item2.GridPosition);
             
             await HandleMatches();
         }

         private async UniTask HandleMatches()
         {
             var matches = _match3Logic.FindMatches();
             while (matches.Count > 0)
             {
                 await DestroyMatchesAsync(matches);
                 
                 //await FallItems();

                 _levelGrid.FillEmptySpaces();
                 
                 await UniTask.Delay(600);
                 matches = _match3Logic.FindMatches();
             }
         }

         private async Task FallItems()
         {
             var fallingItems = _match3Logic.FallDownItems();
             var items = new List<ItemPresenter>();
             foreach (var fallingData in fallingItems)
             {
                 var presenter = _itemRepository.GetPresenter(fallingData.PreviousPosition);
                 _itemRepository.Remove(fallingData.PreviousPosition);
                 _itemRepository.Add(fallingData.NextPosition, presenter);
                 items.Add(_itemRepository.GetPresenter(fallingData.NextPosition));
             }
             
             if (fallingItems.Count > 0)
             {
                 var fallAnimation = new FallAnimation(items);
                 _animationQueue.Enqueue(fallAnimation);
                 await _animationQueue.Execute();
             }
         }

         private async UniTask DestroyMatchesAsync(IEnumerable<Item> matches)
         {
             var matchedItems = matches as Item[] ?? matches.ToArray();
             var itemViews = _itemRepository.GetItemViews(matchedItems);
             var destroyAnimation = new DestroyAnimation(itemViews);
             _animationQueue.Enqueue(destroyAnimation);
             await _animationQueue.Execute();
             
             foreach (var itemView in itemViews)
             {
                 _view.DestroyItem(itemView);
             }
             
             foreach (var item in matchedItems)
             {
                 _itemRepository.Remove(item.GridPosition);
             }
             _match3Logic.RemoveMatches(matchedItems);
         }
    }
}