using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class GameOverSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag>> _bases;
        private readonly EcsSingletonInject<GameCycleData> _gameCycle;
        
        public void Run(IEcsSystems systems)
        {
            if(_gameCycle.Value.isGameOver)
                return;

            if (_bases.Value.GetEntitiesCount() == _gameCycle.Value.maxBases)
                return;
            
            _gameCycle.Value.isGameOver = true;
            Debug.Log("Game Over");
        }
    }
}