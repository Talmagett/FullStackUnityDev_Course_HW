using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public static class MoveUseCase
    {
        public static bool IsMoving(in IEntity entity)
        {
            return entity.GetMoveDirection().Value != Vector3.zero && entity.GetMoveCondition().Value;
        }

        public static void MoveTowards(in IEntity entity, in Vector3 direction, in float deltaTime)
        {
            if (entity.TryGetMoveCondition(out IExpression<bool> condition) && !condition.Value)
                return;

            Transform transform = entity.GetTransform();
            IValue<float> speed = entity.GetMoveSpeed();
            transform.position += direction * (speed.Invoke() * deltaTime);
        }

        public static Vector3 MoveTowards(
            in Vector3 position,
            in float moveSpeed,
            in Vector3 direction,
            in float deltaTime
        )
        {
            return position + direction * (moveSpeed * deltaTime);
        }
    }
}