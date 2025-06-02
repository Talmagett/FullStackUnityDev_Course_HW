using System;
using Leopotam.EcsLite;

namespace SampleGame
{
    [Serializable]
    public struct AttackEvent
    {
        public EcsPackedEntity entity;
    }
}