using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public static class VectorUseCase
    {
        public static Vector3 GetDirectionAt(in IEntity entity, in IEntity target)
        {
            Vector3 currentPosition = entity.GetTransform().position;
            Vector3 targetPosition = target.GetTransform().position;
            Vector3 vector = targetPosition - currentPosition;
            vector.y = 0;
            
            return vector.normalized;
        }
        
        public static Vector3 DistanceVector(in IEntity start, in IEntity end)
        {
            Vector3 currentPosition = start.GetTransform().position;
            Vector3 targetPosition = end.GetTransform().position;
            return targetPosition - currentPosition;
        }

        public static float Distance(in IEntity start, in IEntity end)
        {
            return DistanceVector(start, end).magnitude;
        }

        public static bool IsDistanceGreatest(IEntity entity, IEntity target, float minDistance)
        {
            Vector3 distanceVector = DistanceVector(entity, target);
            return distanceVector.sqrMagnitude > minDistance;
        }
        
        public static bool IsDistanceLessOrEquals(IEntity entity, IEntity target, float minDistance)
        {
            Vector3 distanceVector = DistanceVector(entity, target);
            return distanceVector.sqrMagnitude <= minDistance;
        }
    }
}