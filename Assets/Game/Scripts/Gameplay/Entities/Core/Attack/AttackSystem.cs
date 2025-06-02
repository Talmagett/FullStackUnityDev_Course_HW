using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitAttackRequired>> _filter;
        
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Target> _targets;
        private readonly EcsUseCaseInject<AttackUseCase> _attackUseCase;

        private readonly EcsPoolInject<AttackDistance> _attackDistances;
        private readonly EcsPoolInject<AttackCooldown> _attackCooldowns;
        
        private readonly EcsUseCaseInject<TeamUseCase> _teamUseCase;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref Position position = ref _positions.Value.Get(entity);
                ref Target target = ref _targets.Value.Get(entity);
                ref AttackDistance attackDistance = ref _attackDistances.Value.Get(entity);
                ref AttackCooldown attackCooldown = ref _attackCooldowns.Value.Get(entity);

                if (!_attackUseCase.Value.CanAttack(entity)) 
                    continue;
                
                ref Position targetPosition = ref _positions.Value.Get(target.value);
                float distanceToTarget = Vector3.Distance(position.value, targetPosition.value);
                if (distanceToTarget > attackDistance.value) continue;
                ref UnitAttackRequired required = ref _filter.Pools.Inc1.Get(entity);
                required.value = true;
                attackCooldown.current = attackCooldown.max;
            }
        }
    }
}