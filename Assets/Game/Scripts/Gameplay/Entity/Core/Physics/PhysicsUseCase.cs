using System;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame.Engine
{
    public static class PhysicsUseCase
    {
        private static readonly Collider[] buffer = new Collider[32];
        
        public static bool OverlapSphereFirst(
            in Vector3 center,
            in float radius,
            in LayerMask layerMask,
            in QueryTriggerInteraction triggerInteraction,
            in Predicate<IEntity> predicate,
            out IEntity result
        )
        {
            int size = Physics.OverlapSphereNonAlloc(
                center,
                radius,
                buffer,
                layerMask,
                triggerInteraction
            );

            for (int i = 0; i < size; i++)
            {
                Collider collider = buffer[i];
                if (!collider.TryGetComponent(out IEntity entity))
                    continue;

                if (predicate.Invoke(entity))
                {
                    result = entity;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}