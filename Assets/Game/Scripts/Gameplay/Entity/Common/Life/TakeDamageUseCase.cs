using System;
using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public static class TakeDamageUseCase
    {
        public static bool TakeDamage(in IEntity target, in int damage, IEntity source)
        {
            if (!target.HasDamageableTag())
                return false;

            IReactiveVariable<int> health = target.GetHealth();
            
            int current = health.Value;
            if (current <= 0)
                return false;

            health.Value = Math.Max(0, current - damage);
            return true;
        }
    }
}