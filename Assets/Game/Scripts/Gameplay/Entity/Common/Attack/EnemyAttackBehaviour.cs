using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class EnemyAttackBehaviour : IEntityFixedUpdate
    {
        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            if(entity.GetTarget().Value==null)
                return;
            IEntity target = entity.GetTarget().Value;

            if(!HealthUseCase.IsAlive(target))
                return;
            float attackDistance = entity.GetCurrentWeapon().Value.GetAttackDistance().Value;
            
            if(Vector3.SqrMagnitude(entity.GetTarget().Value.GetTransform().position - entity.GetTransform().position) > (attackDistance*attackDistance))
                return;
            if(!entity.GetFireCondition().Invoke())
                return;
            entity.GetFireRequest().Invoke();
        }
    }
}