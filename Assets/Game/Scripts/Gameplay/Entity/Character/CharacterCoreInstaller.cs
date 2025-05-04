using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CharacterCoreInstaller : SceneEntityInstaller
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
            entity.AddDamageableTag();
            entity.AddMaxHealth(new Const<int>(health));
            entity.AddHealth(new ReactiveVariable<int>(health));
            entity.AddBehaviour<DeathBehaviour>();
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
            entity.AddBehaviour<MoveTowardsBehaviour>();
        }

        private void InstallRotate(IEntity entity)
        {
            entity.AddAngularSpeed(new Const<float>(angularSpeed));
            entity.AddAngularDirection(new ReactiveVector3());
            entity.AddBehaviour<RotateTowardsBehaviour>();
        }

        private void InstallWeapon(IEntity entity)
        {
            entity.AddWeaponContainer(weaponContainer);
            entity.AddCurrentWeapon(new ReactiveVariable<IEntity>(initialWeapon));
        }
    }
}