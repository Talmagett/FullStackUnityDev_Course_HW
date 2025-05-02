using System;
using Leopotam.EcsLite;

namespace SampleGame
{
    [Serializable]
    public struct BulletCollisionRequest
    {
        public EcsPackedEntity bullet;
        public EcsPackedEntity target;
    }
}