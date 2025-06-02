using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public readonly struct AttackUseCase
    {
        private readonly EcsPoolInject<AttackCooldown> _attackCooldowns;
        private readonly EcsPoolInject<Target> _targetPool;
        private readonly EcsPoolInject<Health> _healthPool;
        
        public bool CanAttack(in int entity)
        {
            ref AttackCooldown attackCooldown = ref _attackCooldowns.Value.Get(entity);
            ref Target target = ref _targetPool.Value.Get(entity);
            return attackCooldown.current <= 0 
                   && target.value != -1;
        }

        public void Attack(in int target, in int damage)
        {
            ref Health targetHealth = ref _healthPool.Value.Get(target);
            targetHealth.current -= damage;
        }
    }
}