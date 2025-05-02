using Leopotam.EcsLite;

namespace SampleGame
{
    public struct TakeDamageEvent
    {
        public EcsPackedEntity source;
        public EcsPackedEntity target;
        public int damage;
    }
}