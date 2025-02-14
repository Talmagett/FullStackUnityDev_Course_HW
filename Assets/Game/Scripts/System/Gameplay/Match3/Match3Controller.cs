using System;
using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.System.Gameplay.Quests;
using Game.UI.Game.Match3;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.System.Gameplay.Match3
{
    public class Match3Controller : IInitializable, IDisposable
    {
        private readonly Match3Logic _match3Logic;
        private readonly LevelGridPresenter _levelGridPresenter;
        private readonly AnimationQueue _animationQueue;
        private readonly ItemInputHandler _itemInputHandler;
        private readonly Quest _quest;
        private bool _isGameOver;

        public Match3Controller(Match3Logic match3Logic,ItemInputHandler itemInputHandler, Quest quest)
        {
            _match3Logic = match3Logic;
            _animationQueue = new AnimationQueue();
            _itemInputHandler = itemInputHandler;
            _quest = quest;
        }
/*
        private async UniTask HandleSwipeAsync(ItemView item, Vector2Int direction)
        {
            if (_animationQueue.IsRunning) return;
            if (_quest.IsQuestComplete()) return;

            var item = _levelGridPresenter.GetItem(item);
            if (!_match3Logic.TrySwap(item, direction, out ItemView swappedItem))
                return;
            
            var swipeAnimation = new SwapAnimation(item, swappedItem);
            _animationQueue.Enqueue(swipeAnimation);
            await _animationQueue.Execute();
            
            var matches = _levelController.FindMatches();
            while (matches.Count > 0)
            {
                var destroyAnimation = new DestroyAnimation(matches);
                _animationQueue.Enqueue(destroyAnimation);
                //_soundPlayer.Play(SoundName.Collect);
                await _animationQueue.Execute();

                _levelController.RemoveMatches(matches);
                var fallingItems = _levelController.FallDownItems();
                
                if (fallingItems.Count > 0)
                {
                    var fallAnimation = new FallAnimation(fallingItems);
                    _animationQueue.Enqueue(fallAnimation);
                    await _animationQueue.Execute();
                }

                _levelController.FillEmptySpaces();

                matches = _levelController.FindMatches();
            }
        }
        */
        public void Initialize()
        {
            _itemInputHandler.OnItemSwipe += HandleSwipe;
        }

        private void HandleSwipe(ItemView arg1, Vector2Int arg2)
        {
            //HandleSwipeAsync(arg1, arg2).Forget();
        }

        public void Dispose()
        {
            _itemInputHandler.OnItemSwipe -= HandleSwipe;
        }
    }
}
