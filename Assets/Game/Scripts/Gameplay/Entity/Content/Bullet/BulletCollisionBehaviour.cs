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
            _trigger.OnEntered += OnCollisionEntered;
        }

        public void Dispose(in IEntity entity)
        {
            _trigger.OnEntered -= OnCollisionEntered;
        }

        private void OnCollisionEntered(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                _destroyAction.Invoke();
                return;
            }
            
            if(!collision.TryGetEntity(out IEntity target))
                return;
            
            int damage = _damage.Value;
           
            IEntity owner = _owner.Value;
            if (owner != null && owner.TryGetExtraDamage(out IExpression<int> extraDamage))
                damage += extraDamage.Value;
            
            if (target!=owner && TakeDamageUseCase.TakeDamage(target, damage, owner))
                _destroyAction.Invoke();
        }
    }
}