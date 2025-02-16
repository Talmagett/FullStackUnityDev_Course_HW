using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay.Items;
using Game.UI.Game.Items;
using Game.UI.Game.Quest;
using Modules.Animations;
using UnityEngine;

namespace Game.UI.Game.Animations
{
    public class QuestItemAnimation : IAnimation
    {
        private readonly List<ItemPresenter> _items;
        private readonly QuestAnimationPresenter _questAnimationPresenter;

        public QuestItemAnimation(List<ItemPresenter> items, QuestAnimationPresenter questAnimationPresenter)
        {
            _items = items;
            _questAnimationPresenter = questAnimationPresenter;
        }

        public async UniTask Execute()
        {
            List<UniTask> tasks = new();
            foreach (var item in _items)
            {
                tasks.Add(_questAnimationPresenter.CreateItem(item.Item.ItemType, item.ItemView.transform.position));
            }
            await UniTask.WhenAll(tasks);
        }
    }
}