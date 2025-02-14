using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;

namespace Game.Common
{
    public class FallAnimation : IAnimation
    {
        private readonly HashSet<ItemView> _fallingItems;
        private const float dropSpeed=20;

        public FallAnimation(HashSet<ItemView> fallingItems)
        {
            _fallingItems = fallingItems;
        }

        public async UniTask Execute()
        {
            var tasks = new List<UniTask>();
            foreach (var item in _fallingItems)
            {
                //tasks.Add(item.DropDown(item.GridPosition,dropSpeed));
            }
            await UniTask.WhenAll(tasks);
        }
    }
}