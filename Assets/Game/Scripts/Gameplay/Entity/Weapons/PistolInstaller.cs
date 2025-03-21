using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entity.Weapons
{
    public class PistolInstaller : SceneEntityInstaller
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private Ammo ammo;
        [SerializeField] private SceneEntity pickupPrefab;
        
        public override void Install(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;

            entity.AddGameObject(gameObject);
            entity.AddTransform(transform);
            entity.AddPickUpPrefab(pickupPrefab);

            InstallFire(entity, gameContext);
        }

        private void InstallFire(IEntity entity, GameContext gameContext)
        {
            entity.AddAmmo(ammo);
            entity.AddFirePoint(firePoint);
            entity.AddFireEvent(new BaseEvent());
            entity.AddFireCondition(new AndExpression(ammo.Exists));
            entity.AddFireAction(new BaseAction(() =>
            {
                if (entity.GetFireCondition().Invoke())
                {
                    FireBulletUseCase.FireBullet(entity, gameContext);
                    ammo.Spend();
                    entity.GetFireEvent().Invoke();
                }
            }));
        }
    }
}