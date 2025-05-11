using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Common;
using SampleGame;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class AttackJoystick : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        private IEntity _playerEntity;
        private IAction _fireRequest;

        private void Awake()
        {
            _playerEntity = GameContext.Instance.GetPlayerCharacter();
            _fireRequest = _playerEntity.GetFireAction();
        }

        private void Update()
        {
            if(joystick.IsPressed)
            {                
                var rotate = _playerEntity.GetAngularDirection();
                var direction=new Vector3(joystick.Direction.x,0,joystick.Direction.y);
                rotate.Value=direction;
                _fireRequest?.Invoke();
            }
        }
    }
}