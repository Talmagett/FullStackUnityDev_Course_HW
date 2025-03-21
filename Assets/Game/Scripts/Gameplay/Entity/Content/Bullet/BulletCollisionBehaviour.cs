using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class BulletCollisionBehaviour : IEntityInit, IEntityDispose
    {
        private IAction _destroyAction;
        private CollisionEventReceiver _trigger;
        private IValue<int> _damage;
        private IValue<IEntity> _owner;

        public void Init(in IEntity entity)
        {
            _destroyAction = entity.GetDestroyAction();
            _damage = entity.GetDamage();
            _owner = entity.GetOwner();

            _trigger = entity.GetColliderReceiver();
            _trigger.OnEntered += this.OnTriggerEntered;
        }

        public void Dispose(in IEntity entity)
        {
            _trigger.OnEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(Collision collider)
        {
            int damage = _damage.Value;
           
            IEntity owner = _owner.Value;
            if (owner != null && owner.TryGetExtraDamage(out IExpression<int> extraDamage))
                damage += extraDamage.Value;
            
            if (collider.collider.TryGetComponent(out IEntity target)&&target!=owner && TakeDamageUseCase.TakeDamage(target, damage, owner))
                _destroyAction.Invoke();
        }
    }
}