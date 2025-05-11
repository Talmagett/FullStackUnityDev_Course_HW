using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class EnemyCoreInstaller : SceneEntityInstaller
    {
        [SerializeField] private Rigidbody rigidbody;
        
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float angularSpeed = 15;
        [SerializeField] private int health = 10;
        [SerializeField] private SceneEntity initialWeapon;
        [SerializeField] private Transform weaponContainer;
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        
        public override void Install(IEntity entity)
        {
            InstallMain(entity);
            InstallLife(entity);
            InstallCombat(entity);
            InstallMove(entity);
            InstallRotate(entity);
            InstallWeapon(entity);
            
            entity.AddPlayerTag();
            entity.AddMoveableTag();
        }

        private void InstallMain(IEntity entity)
        {
            entity.AddGameObject(gameObject);
            entity.AddTransform(transform);
            entity.AddRigidbody(rigidbody);
            entity.AddTriggerReceiver(triggerEventReceiver);
        }

        private void InstallLife(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;
            entity.AddDamageableTag();
            entity.AddHealth(new Health(health,health));
            entity.AddDamageTakenEvent(new BaseEvent<TakeDamageArgs>());
            entity.AddDeathEvent(new BaseEvent());
            entity.AddBehaviour<DeathBehaviour>();
            entity.AddBehaviour(new KillScoreBehaviour(gameContext));
        }

        private void InstallCombat(IEntity entity)
        {
            entity.AddFireEvent(new BaseEvent());
            entity.AddFireCondition(new AndExpression(
                () => HealthUseCase.IsAlive(entity),
                () =>
                {
                    IEntity weapon = entity.GetCurrentWeapon().Value;
                    return weapon != null && weapon.GetFireCondition().Invoke();
                }
            ));
            entity.AddTarget(new ReactiveVariable<IEntity>());
            entity.AddFireAction(new BaseAction(() =>
            {
                if (entity.GetFireCondition().Invoke())
                {
                    entity.GetCurrentWeapon().Value?.GetFireAction().Invoke();
                    entity.GetFireEvent().Invoke();
                }
            }));
        }

        private void InstallMove(IEntity entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveSpeed(new ReactiveFloat(moveSpeed));
            entity.AddMoveDirection(new ReactiveVector3());
            entity.AddMoveCondition(new AndExpression(() => HealthUseCase.IsAlive(entity)));
            entity.AddBehaviour<MoveToTargetBehaviour>();
        }

        private void InstallRotate(IEntity entity)
        {
            entity.AddAngularSpeed(new Const<float>(angularSpeed));
            entity.AddAngularDirection(new ReactiveVector3());
            entity.AddRotateCondition(new AndExpression(() => HealthUseCase.IsAlive(entity)));
            entity.AddBehaviour<RotateToTargetBehaviour>();
        }

        private void InstallWeapon(IEntity entity)
        {
            entity.AddWeaponContainer(weaponContainer);
            entity.AddCurrentWeapon(new ReactiveVariable<IEntity>(initialWeapon));
        }
    }
}