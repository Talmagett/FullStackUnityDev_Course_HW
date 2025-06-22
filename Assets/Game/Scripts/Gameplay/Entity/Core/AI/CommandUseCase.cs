using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Gameplay
{
    public static class CommandUseCase
    {
        public static void Attack(IEntity character, RaycastHit raycastHit, bool additive)
        {
            if (IsTargetEntity(character,raycastHit, out var targetEntity))
                character.GetTarget().Value = targetEntity;
        }

        public static void Follow(IEntity character, RaycastHit raycastHit, bool additive)
        {
            if (IsTargetEntity(character,raycastHit, out var targetEntity))
                character.GetTarget().Value = targetEntity;
        }

        public static void Hold(IEntity character, bool additive)
        {
            throw new NotImplementedException();
        }

        public static void Move(IEntity character, RaycastHit raycastHit, bool additive)
        {
            if (IsTargetEntity(character, raycastHit, out var targetEntity))
            {
                Debug.Log($"Moving {character} to {targetEntity}");
                character.GetTarget().Value = targetEntity;
            }
        }

        public static void Patrol(IEntity character, RaycastHit raycastHit, bool additive)
        {
            if (IsTargetEntity(character, raycastHit, out var targetEntity))
                return;
        }

        public static void Stop(IEntity character)
        {
            //abort all commands, then Hold
            Hold(character, false);
        }

        private static bool IsTargetEntity(IEntity myEntity, RaycastHit hit, out IEntity targetEntity)
        {
            targetEntity = null;
            return hit.collider != null && hit.collider.TryGetEntity(out targetEntity) && targetEntity != myEntity;
        }
    }
}