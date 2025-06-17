using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class WeaponCoreInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private Cooldown _cooldown = new(1);

        public override void Install(IEntity entity)
        {
            entity.AddTeam(new ReactiveVariable<TeamType>(TeamType.NEUTRAL));

            entity.AddFirePoint(_firePoint);
            entity.AddFireRequest(new BaseRequest());
            entity.AddFireCondition(new BaseFunction<bool>(_cooldown.IsExpired));
            entity.AddFireEvent(new BaseEvent());
            entity.AddBehaviour(new FireBehaviour(
                action: () =>
                {
                    FireBulletUseCase.FireBullet(entity, GameContext.Instance);
                    _cooldown.Reset();
                }
            ));

            entity.WhenFixedUpdate(_cooldown.Tick);
        }
    }
}