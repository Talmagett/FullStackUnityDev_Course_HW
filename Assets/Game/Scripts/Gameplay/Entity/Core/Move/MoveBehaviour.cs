using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class MoveBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private readonly Func<Vector3, float, bool> _condition;
        private readonly Action<Vector3, float> _action;

        private IRequest<Vector3> _moveRequest;
        private IEvent<Vector3> _moveEvent;

        public MoveBehaviour(Func<Vector3, float, bool> condition, Action<Vector3, float> action)
        {
            _condition = condition;
            _action = action;
        }
        
        public void Init(in IEntity entity)
        {
            _moveRequest = entity.GetMoveRequest();
            _moveEvent = entity.GetMoveEvent();
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            if (!_moveRequest.Consume(out Vector3 direction))
                return;

            if (direction == Vector3.zero) 
                return;

            if (!_condition.Invoke(direction, deltaTime))
                return;
            
            _action.Invoke(direction, deltaTime);
            _moveEvent.Invoke(direction);
        }
    }
}