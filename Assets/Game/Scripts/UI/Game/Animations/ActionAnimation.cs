using System;
using Cysharp.Threading.Tasks;
using Modules.Animations;

namespace Game.UI.Game.Animations
{
    public class ActionAnimation : IAnimation
    {
        private event Action action;

        public ActionAnimation(Action swapItems)
        {
            action = swapItems;
        }

        public async UniTask Execute()
        {
            await UniTask.Yield();
            action?.Invoke();
        }
    }
}