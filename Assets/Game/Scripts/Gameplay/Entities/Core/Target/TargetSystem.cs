using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class TargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Target>> _filter;
        private readonly EcsFilterInject<Inc<Health>> _allUnits;
        
        private readonly EcsUseCaseInject<HealthUseCase> _healthUseCase;
        private readonly EcsPoolInject<Health> _healths;
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        private readonly EcsPoolInject<Target> _targets;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                // if(_targets.Value.Get(entity).value != -1)
                //     continue;
                Debug.Log("TargetSystem: has no enemy");
                // if(_healthUseCase.Value.Exists(entity))
                //     continue;
                Debug.Log("TargetSystem: searching enemy");
                ref Position myPos  = ref _positions.Value.Get(entity);
                ref TeamType myTeam = ref _teamTypes.Value.Get(entity);
                
                int closest = TargetUseCase.FindClosestEnemy(
                    in myPos.value,
                    myTeam,
                    _allUnits.Value,
                    _positions.Value, 
                    _teamTypes.Value,
                    _healths.Value
                );

                ref var target = ref _targets.Value.Get(entity);
                target.value = closest;
                Debug.DrawLine(_positions.Value.Get(entity).value,
                    _positions.Value.Get(closest).value,
                    Color.red, 0.1f, false);
            }
        }
    }
}