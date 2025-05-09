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
        [SerializeField] private float fireCooldown=0.5f;
        [SerializeField] private int damage;
        [SerializeField] private float attackRadius=0.2f;

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
            entity.AddFireEvent(new BaseEvent());
            entity.AddFireCondition(new AndExpression(cooldown.IsExpired));
            entity.AddFireAction(new BaseAction(() =>
            {
                if (entity.GetFireCondition().Invoke())
                {
                    //FireBulletUseCase.FireBullet(entity, gameContext);
                    entity.GetFireEvent().Invoke();
                    cooldown.Reset();
                }
            }));
        }
    }
}