using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace SampleGame
{
    public sealed class UnitSpawnSystem : IEcsRunSystem
    {
        private readonly EcsEventInject<UnitSpawnRequest> _requests;

        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Rotation> _rotations;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        private readonly EcsPoolInject<MoveDirection> _moveDirections;

        public void Run(IEcsSystems systems)
        {
            while (_requests.Value.Consume(out UnitSpawnRequest request)) 
                this.SpawnUnit(request);
        }

        private void SpawnUnit(UnitSpawnRequest spawnRequest)
        {
            EcsPrototype unitPrefab = spawnRequest.prefab;
            int unit = unitPrefab.Create(_world.Value);
            
            _positions.Value.Add(unit).value = spawnRequest.position;
            _rotations.Value.Add(unit).value = spawnRequest.rotation;
            _teamTypes.Value.Add(unit) = spawnRequest.team;
            
            _moveDirections.Value.Get(unit).value = math.mul(spawnRequest.rotation, new float3(0, 0, 1));
        }
    }
}