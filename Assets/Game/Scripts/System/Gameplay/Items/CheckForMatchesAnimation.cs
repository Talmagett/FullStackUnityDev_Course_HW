using System;
using Cysharp.Threading.Tasks;
using Modules.Animations;

namespace Game.Common
{
    public class CheckForMatchesAnimation : IAnimation
    {
        private Func<bool> _checkGrid;
        public CheckForMatchesAnimation(Func<bool> action)
        {
            _checkGrid = action;
        }
        public async UniTask Execute()
        {
            await UniTask.Yield();
            _checkGrid.Invoke();
        }
    }
}