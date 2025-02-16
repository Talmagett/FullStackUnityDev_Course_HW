using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.UI.Game.Animations
{
    public class FallAnimation : IAnimation
    {
        private readonly List<ItemPresenter> _viewsToAnimate;
        private readonly List<Vector2Int> _newPositions;
        private const float DropSpeed=20;

        public FallAnimation(List<ItemPresenter> viewsToAnimate, List<Vector2Int> newPositions)
        {
            _viewsToAnimate = viewsToAnimate;
            _newPositions = newPositions;
        }

        public async UniTask Execute()
        {
            var tasks = new List<UniTask>();
            for (int i = 0; i < _viewsToAnimate.Count; i++)
            {
                tasks.Add(_viewsToAnimate[i].FallDown(_newPositions[i],DropSpeed));
            }
            await UniTask.WhenAll(tasks);
        }
    }
}