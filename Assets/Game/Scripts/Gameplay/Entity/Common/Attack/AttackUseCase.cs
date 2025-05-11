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

            foreach (var hit in hits)
            {
                if(!hit.TryGetEntity(out IEntity target)) return;
                if(!target.HasDamageableTag()) return;
                if(target== weapon.GetOwner().Value) return;
                TakeDamageUseCase.TakeDamage(target, damage.Value, weapon.GetOwner().Value);
                return;
            }
        }
    }
}