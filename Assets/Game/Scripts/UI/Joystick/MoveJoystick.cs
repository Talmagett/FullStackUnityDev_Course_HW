using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Common;
using SampleGame;
using UnityEngine;

public class MoveJoystick : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    private IEntity _playerEntity;
    
    void Awake()
    {
        _playerEntity = GameContext.Instance.GetPlayerCharacter();
    }

    private void Update()
    {
        var move = _playerEntity.GetMoveDirection();
        var rotate = _playerEntity.GetAngularDirection();
        var direction=new Vector3(joystick.Direction.x,0,joystick.Direction.y);
        move.Value=direction;
        rotate.Value=direction;
    }
}
