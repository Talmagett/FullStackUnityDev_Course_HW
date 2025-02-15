using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.Scripts.UI.Game.Match3;
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
            _levelGrid = levelGrid;
            _match3Logic = match3Logic;
            _view = view;
            _itemSpriteMap = itemSpriteMap;
            _itemRepository = new ItemRepository();
            _animationQueue = new AnimationQueue();
            _itemInputHandler = itemInputHandler;
        }
        
        public void Initialize()
        {
            _view.Inititalize(_levelGrid.GridSize);
            foreach (var item in _levelGrid.GetAllItems())
            {
                var itemSprite = _itemSpriteMap.GetItemSprite(item.ItemType);
                var itemView = _view.SpawnItem(item.GridPosition, itemSprite);
                var itemPresenter = new ItemPresenter(item, itemView);
                _itemRepository.Add(item.GridPosition,itemPresenter);
            }
            _itemInputHandler.OnItemSwipe+= HandleSwipe;
        }

        public void Dispose()
        {
            _itemInputHandler.OnItemSwipe-= HandleSwipe;
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

            if (!_match3Logic.TrySwap(item1.GridPosition, item2.GridPosition))
                return;
            
            var itemPresenter1 = _itemRepository.GetPresenter(item1.GridPosition);
            var itemPresenter2 = _itemRepository.GetPresenter(item2.GridPosition);

            var swapAnimation = new SwapAnimation(itemPresenter1.ItemView, itemPresenter2.ItemView);
            _animationQueue.Enqueue(swapAnimation);
            await _animationQueue.Execute();

            _itemRepository.Remove(item1.GridPosition);
            _itemRepository.Remove(item2.GridPosition);
            _itemRepository.Add(item1.GridPosition, itemPresenter2);
            _itemRepository.Add(item2.GridPosition, itemPresenter1);
            
            await HandleMatches();
        }

        private async UniTask HandleMatches()
        {
            var matches = _match3Logic.FindMatches();
            while (matches.Count > 0)
            {
                var itemViews = _itemRepository.GetItemViews(matches);
                var destroyAnimation = new DestroyAnimation(itemViews);
                _animationQueue.Enqueue(destroyAnimation);
                await _animationQueue.Execute();

                foreach (var item in matches)
                {
                    _itemRepository.Remove(item.GridPosition);
                }

                _match3Logic.RemoveMatches(matches);
                /*var fallingItems = _match3Logic.FallDownItems();

                if (fallingItems.Count > 0)
                {
                    var fallAnimation = new FallAnimation(fallingItems);
                    _animationQueue.Enqueue(fallAnimation);
                    await _animationQueue.Execute();
                }*/

                _levelGrid.FillEmptySpaces();
                matches = _match3Logic.FindMatches();
            }
        }
    }
}