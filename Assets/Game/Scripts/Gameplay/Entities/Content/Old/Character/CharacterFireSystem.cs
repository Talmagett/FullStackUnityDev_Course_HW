using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace SampleGame
{
    public sealed class CharacterFireSystem : IEcsRunSystem
    {
        private readonly EcsPrototype _bulletPrefab;
        
        private readonly EcsFilterInject<Inc<CharacterTag>> _characters;
        private readonly EcsPoolInject<UnitAttackRequired> _fireRequires;
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Rotation> _rotations;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        private readonly EcsPoolInject<FireOffset> _fireOffsets;

        private readonly EcsEventInject<BulletSpawnRequest> _spawnRequests;
        private readonly EcsEventInject<FireEvent> _fireEvents;
        private readonly EcsWorldInject _world;

        public CharacterFireSystem(EcsPrototype bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in _characters.Value)
            {
                ref UnitAttackRequired attackRequired = ref _fireRequires.Value.Get(entity);
                if (!attackRequired.value) 
                    continue;

                float3 position = _positions.Value.Get(entity).value;
                quaternion rotation = _rotations.Value.Get(entity).value;
                float3 offset = _fireOffsets.Value.Get(entity).value;

                _spawnRequests.Value.Fire(new BulletSpawnRequest
                {
                    prefab = _bulletPrefab,
                    position = position + math.mul(rotation, offset),
                    rotation = rotation,
                    team = _teamTypes.Value.Get(entity)
                });
                
                _fireEvents.Value.Fire(new FireEvent{entity = _world.Value.PackEntity(entity)});
            }
        }
    }
}