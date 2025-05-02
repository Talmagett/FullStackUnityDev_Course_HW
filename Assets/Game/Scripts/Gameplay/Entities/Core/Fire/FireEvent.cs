using System;
using Leopotam.EcsLite;

namespace SampleGame
{
    [Serializable]
    public struct FireEvent
    {
        public EcsPackedEntity entity;
    }
}