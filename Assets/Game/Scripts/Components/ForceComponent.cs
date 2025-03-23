using System;
using System.Linq;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class ForceComponent : MonoBehaviour
    {
        public event Action OnPush;
        [SerializeField] private float pushPower;
        [SerializeField] private ForceDetector forceDetector;
        [SerializeField] private Cooldown cooldown;
        private readonly AndCondition _andCondition = new();

        private void Update()
        {
            cooldown.TickUpdate(Time.deltaTime);
        }

        public void Force(Vector3 direction)
        {
            if (!_andCondition.IsTrue()|| !cooldown.IsReady())
                return;

            var targets = forceDetector.GetPushingTargets();
            cooldown.Reload();
            OnPush?.Invoke();

            if (!targets.Any())
                return;
            direction.Normalize();
            foreach (var pushingTarget in targets)
            {
                if (pushingTarget.gameObject == gameObject) continue;
                pushingTarget.AddForce(direction*pushPower, ForceMode2D.Impulse);
            }
        }
        
        public void Force(Rigidbody2D target, Vector3 direction)
        {
            if (!_andCondition.IsTrue())
                return;
            direction.Normalize();
            OnPush?.Invoke();
            target.AddForce(direction*pushPower, ForceMode2D.Impulse);
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}