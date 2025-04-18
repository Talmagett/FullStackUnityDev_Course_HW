using Atomic.Elements;
using Atomic.Entities;
using Modules.Common;
using SampleGame;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class AttackJoystick : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private SceneEntity playerEntity;
        private IAction _fireRequest;
        private void Awake()
        {
            _fireRequest = playerEntity.GetFireAction();
        }

        private void Update()
        {
            if(joystick.IsPressed)
            {
                _fireRequest?.Invoke();
            }
        }
    }
}