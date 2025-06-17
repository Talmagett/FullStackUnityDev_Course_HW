using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class LookBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private readonly Func<Vector3, float, bool> _condition;
        private readonly Action<Vector3, float> _action;

        private IRequest<Vector3> _lookRequest;

        public LookBehaviour(Func<Vector3, float, bool> condition, Action<Vector3, float> action)
        {
            _condition = condition;
            _action = action;
        }

        public void Init(in IEntity entity)
        {
            _lookRequest = entity.GetLookRequest();
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            if (!_lookRequest.Consume(out Vector3 direction))
                return;

            if (!_condition.Invoke(direction, deltaTime))
                return;

            _action.Invoke(direction, deltaTime);
        }
    }
}