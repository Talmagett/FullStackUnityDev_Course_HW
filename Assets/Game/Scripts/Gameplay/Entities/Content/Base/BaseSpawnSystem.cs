using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace SampleGame
{
    public sealed class BaseSpawnSystem : IEcsRunSystem
    {
        private readonly EcsPrototypeCatalog _prototypeCatalog;
        private readonly EcsFilterInject<Inc<BaseTag>> _bases;
        private readonly EcsPoolInject<UnitSpawnRequired> _spawnRequires;
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Rotation> _rotations;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        private readonly EcsPoolInject<FireOffset> _fireOffsets;

        private readonly EcsEventInject<UnitSpawnRequest> _spawnRequests;
        private readonly EcsEventInject<FireEvent> _fireEvents;
        private readonly EcsWorldInject _world;

        public BaseSpawnSystem(EcsPrototypeCatalog prototypeCatalog)
        {
            _prototypeCatalog = prototypeCatalog;
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (int entity in _bases.Value)
            {
                ref UnitSpawnRequired fireRequired = ref _spawnRequires.Value.Get(entity);
                if (!fireRequired.value)
                    continue;

                float3 position = _positions.Value.Get(entity).value;
                quaternion rotation = _rotations.Value.Get(entity).value;
                float3 offset = _fireOffsets.Value.Get(entity).value;
                var offsetZ = Random.Range(-2, 2);
                
                _spawnRequests.Value.Fire(new UnitSpawnRequest
                {
                    prefab = _prototypeCatalog.GetPrototype(fireRequired.type.ToString()),
                    position = position + math.mul(rotation, offset)+ new float3(0, 0, offsetZ),
                    rotation = rotation,
                    team = _teamTypes.Value.Get(entity)
                });
                fireRequired.value=false;
                fireRequired.type = UnitSpawnType.None;
                //_fireEvents.Value.Fire(new FireEvent{entity = _world.Value.PackEntity(entity)});
            }
        }
    }
}