using Atomic.Entities;
using Modules.Gameplay;

namespace SampleGame
{
    public static class HealthUseCase
    {
        public static bool IsAlive(in IEntity entity)
        {
            return !entity.GetHealth().IsEmpty();
        }
        
        public static bool Heal(in IEntity target, in int healAmount)
        {
            if (!target.TryGetHealth(out Health health))
                return false;
            
            return health.Add(healAmount);
        }
    }
}