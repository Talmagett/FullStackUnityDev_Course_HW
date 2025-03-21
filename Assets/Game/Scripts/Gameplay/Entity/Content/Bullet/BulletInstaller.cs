using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class BulletInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private float moveSpeed = 3;

        [SerializeField]
        private int damage;
        
        [SerializeField]
        private CollisionEventReceiver trigger;

        [SerializeField]
        private float lifetime;

        public override void Install(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;

            entity.AddTransform(transform);
            entity.AddGameObject(gameObject);
            
            entity.AddDamage(new ReactiveInt(damage));
            entity.AddOwner(new ReactiveVariable<IEntity>());
            
            entity.AddLifetime(new Cooldown(lifetime, lifetime));
            entity.AddDestroyAction(new BaseAction(() => SpawnBulletUseCase.UnspawnBullet(gameContext, entity)));
            
            entity.AddMoveSpeed(new ReactiveFloat(moveSpeed));
            entity.AddMoveDirection(new ReactiveVariable<Vector3>());
            entity.AddColliderReceiver(trigger);
            
            entity.AddBehaviour<BulletLifetimeBehaviour>();
            entity.AddBehaviour<MoveTowardsBehaviour>();
            entity.AddBehaviour<BulletCollisionBehaviour>();
        }
    }
}