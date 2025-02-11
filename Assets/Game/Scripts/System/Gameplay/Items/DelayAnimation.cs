using Cysharp.Threading.Tasks;
using Modules.Animations;

namespace Game.Common
{
    public class DelayAnimation : IAnimation
    {
        private readonly int _seconds;
        public DelayAnimation(float seconds)
        {
            _seconds = (int)(seconds * 1000);
        }
        public async UniTask Execute()
        {
            await UniTask.Delay(_seconds);
        }
    }
}