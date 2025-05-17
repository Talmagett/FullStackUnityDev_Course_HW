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
            var hits = Physics.OverlapSphere(firePoint.position, attackRadius.Value);
            var damage = weapon.GetDamage();
            var owner = weapon.GetOwner();
            foreach (var hit in hits)
            {
                if(!hit.TryGetEntity(out IEntity target)) continue;
                if(!target.HasDamageableTag()) continue;
                if(target.GetTeamType()==owner.Value.GetTeamType()) continue;
                TakeDamageUseCase.TakeDamage(target, damage.Value, owner.Value, TakeDamageArgs.DamageType.Melee);
                return;
            }
        }
    }
}