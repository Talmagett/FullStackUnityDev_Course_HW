using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace SampleGame
{
    public sealed class BulletSpawnSystem : IEcsRunSystem
    {
        private readonly EcsEventInject<BulletSpawnRequest> _requests;

        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Rotation> _rotations;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        private readonly EcsPoolInject<MoveDirection> _moveDirections;

        public void Run(IEcsSystems systems)
        {
            while (_requests.Value.Consume(out BulletSpawnRequest request)) 
                this.SpawnBullet(request);
        }

        private void SpawnBullet(BulletSpawnRequest spawnRequest)
        {
            EcsPrototype bulletPrefab = spawnRequest.prefab;
            int bullet = bulletPrefab.Create(_world.Value);
            
            _positions.Value.Add(bullet).value = spawnRequest.position;
            _rotations.Value.Add(bullet).value = spawnRequest.rotation;
            _teamTypes.Value.Add(bullet) = spawnRequest.team;
            
            _moveDirections.Value.Get(bullet).value = math.mul(spawnRequest.rotation, new float3(0, 0, 1));
        }
    }
}