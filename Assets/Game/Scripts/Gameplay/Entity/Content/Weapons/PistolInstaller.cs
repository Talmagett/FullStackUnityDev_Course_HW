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
        [SerializeField] private float fireSpreadAngle = 0.25f;
        [SerializeField] private float fireCooldown=0.5f;
        [SerializeField] private float attackDistance = 4;
        
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
            
            Cooldown cooldown = new Cooldown(fireCooldown);
            entity.AddFireCooldown(cooldown);
            entity.WhenFixedUpdate(cooldown.Tick);
            
            entity.AddFirePoint(firePoint);
            entity.AddAttackDistance(new BaseVariable<float>(attackDistance));
            entity.AddFireEvent(new BaseEvent());
            entity.AddFireCondition(new AndExpression(ammo.Exists, cooldown.IsExpired));
            entity.AddFireAction(new BaseAction(() =>
            {
                if (entity.GetFireCondition().Invoke())
                {
                    var angle = Random.Range(-fireSpreadAngle, fireSpreadAngle)/2;
                    entity.GetFirePoint().transform.localEulerAngles = new Vector3(0, Random.Range(-angle,angle), 0);
                    FireBulletUseCase.FireBullet(entity, gameContext);
                    ammo.Spend();
                    entity.GetFireEvent().Invoke();
                    cooldown.Reset();
                }
            }));
        }
    }
}