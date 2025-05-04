using System;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace SampleGame
{
    [Serializable]
    public struct UnitSpawnRequest
    {
        public EcsPrototype prefab;
        public float3 position;
        public quaternion rotation;
        public TeamType team;
    }
}