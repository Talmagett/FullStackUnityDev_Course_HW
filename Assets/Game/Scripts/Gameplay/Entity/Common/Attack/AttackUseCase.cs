using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using UnityEngine;

namespace SampleGame
{
    public static class AttackUseCase
    {
        public static void Attack(in IEntity weapon, in IGameContext context)
        {
            Transform firePoint = weapon.GetFirePoint();
            var attackRadius = weapon.GetAttackRadius();
            var hits = Physics.OverlapSphere(firePoint.position, attackRadius.Value, LayerMask.GetMask("Enemy"));
            var damage = weapon.GetDamage();
            var owner = weapon.GetOwner();
            
            foreach (var hit in hits)
            {
                if(!hit.TryGetEntity(out IEntity target)) return;
                if(!target.HasDamageableTag()) return;
                if(target== owner.Value) return;
                if(target.GetTeamType()==owner.Value.GetTeamType()) return;
                TakeDamageUseCase.TakeDamage(target, damage.Value, owner.Value);
                return;
            }
        }
    }
}