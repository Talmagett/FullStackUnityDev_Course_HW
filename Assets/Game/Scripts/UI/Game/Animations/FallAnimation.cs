using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
{
    public class FallAnimation : IAnimation
    {
        private readonly IEnumerable<ItemPresenter> _fallingItems;
        private const float dropSpeed=20;

        public FallAnimation(IEnumerable<ItemPresenter> fallingItems)
        {
            _fallingItems = fallingItems;
        }

        public async UniTask Execute()
        {
            var tasks = new List<UniTask>();
            foreach (var item in _fallingItems)
            {
                tasks.Add(item.FallDown(dropSpeed));
            }
            await UniTask.WhenAll(tasks);
        }
    }
}