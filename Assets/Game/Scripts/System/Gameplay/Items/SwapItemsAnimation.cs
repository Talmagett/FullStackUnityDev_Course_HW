using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
{
    public class SwapItemsAnimation : IAnimation
    {
        private readonly ItemView item1;
        private readonly ItemView item2;

        public SwapItemsAnimation(ItemView item1, ItemView item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
        
        public async UniTask Execute()
        {
            await UniTask.WhenAll(
                item1.MoveTo(item2.GridPosition),
                item2.MoveTo(item1.GridPosition));
        }
    }
}