using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entity.Weapons
{
    public class HandInstaller : SceneEntityInstaller
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireCooldown=0.5f;
        [SerializeField] private int damage;
        [SerializeField] private float attackRadius=1f;
        [SerializeField] private float attackDistance=0.5f;

        public override void Install(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;

            entity.AddGameObject(gameObject);
            entity.AddTransform(transform);

            InstallFire(entity, gameContext);
        }

        private void InstallFire(IEntity entity, GameContext gameContext)
        {            
            Cooldown cooldown = new Cooldown(fireCooldown);
            entity.AddFireCooldown(cooldown);
            entity.WhenFixedUpdate(cooldown.Tick);
            
            entity.AddDamage(new ReactiveInt(damage));
            entity.AddFirePoint(firePoint);
            entity.AddFireEvent(new BaseEvent());
            entity.AddAttackDistance(new BaseVariable<float>(attackDistance));
            entity.AddAttackRadius(new BaseVariable<float>(attackRadius));
            entity.AddFireCondition(new AndExpression(cooldown.IsExpired));
            entity.AddOwner(new ReactiveVariable<IEntity>(entity));
            entity.AddFireAction(new BaseAction(() =>
            {
                if (entity.GetFireCondition().Invoke())
                {
                    AttackUseCase.Attack(entity, gameContext);
                    entity.GetFireEvent().Invoke();
                    cooldown.Reset();
                }
            }));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position,attackRadius);
        }
    }
}