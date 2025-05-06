using System;
using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;

namespace SampleGame
{
    public static class TakeDamageUseCase
    {
        public static bool TakeDamage(in IEntity target, in int damage, IEntity source)
        {
            if (!target.HasDamageableTag())
                return false;

            Health health = target.GetHealth();
            
            int current = health.GetCurrent();
            if (current <= 0)
                return false;

            health.Reduce(damage);
            return true;
        }
    }
}