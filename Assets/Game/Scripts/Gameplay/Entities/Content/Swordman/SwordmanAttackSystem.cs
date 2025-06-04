using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class SwordmanAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SwordmanTag>> _characters;
        private readonly EcsPoolInject<UnitAttackRequired> _fireRequires;
        private readonly EcsPoolInject<Target> _positions;
        private readonly EcsPoolInject<Health> _rotations;
        private readonly EcsPoolInject<Damage> _damage;
        private readonly EcsEventInject<AttackEvent> _attackEvents;
        private readonly EcsWorldInject _world;
        private readonly EcsUseCaseInject<TakeDamageUseCase> _takeDamageUseCase;
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in _characters.Value)
            {
                ref UnitAttackRequired attackRequired = ref _fireRequires.Value.Get(entity);
                if (!attackRequired.value) 
                    continue;

                ref Target target = ref _positions.Value.Get(entity);
                attackRequired.value = false;
                if (target.value == -1)
                    continue;
                
                ref Damage damage = ref _damage.Value.Get(entity);
                ref Health health = ref _rotations.Value.Get(target.value);
                health.current -= damage.value;

                var myViewPack = _world.Value.PackEntity(entity);
                var targetPack = _world.Value.PackEntity(target.value);
                if (_takeDamageUseCase.Value.TakeDamage(myViewPack, targetPack))
                    _attackEvents.Value.Fire(new AttackEvent{entity = myViewPack});
            }
        }
    }
}