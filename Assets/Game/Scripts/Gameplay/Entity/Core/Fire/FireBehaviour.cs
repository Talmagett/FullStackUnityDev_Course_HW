using System;
using Atomic.Elements;
using Atomic.Entities;

namespace Game.Gameplay
{
    public sealed class FireBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private readonly Action _action;

        private IRequest _fireRequest;
        private IFunction<bool> _fireCondition;
        private IEvent _fireEvent;

        public FireBehaviour(Action action)
        {
            _action = action;
        }

        public void Init(in IEntity entity)
        {
            _fireRequest = entity.GetFireRequest();
            _fireCondition = entity.GetFireCondition();
            _fireEvent = entity.GetFireEvent();
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            if (!_fireRequest.Consume())
                return;

            if (!_fireCondition.Invoke())
                return;
            
            _action.Invoke();
            _fireEvent.Invoke();
        }
    }
}