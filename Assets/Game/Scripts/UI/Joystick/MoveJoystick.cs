using Atomic.Entities;
using Modules.Common;
using SampleGame;
using UnityEngine;

public class MoveJoystick : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private SceneEntity playerEntity;
    
    private void Update()
    {
        var move = playerEntity.GetMoveDirection();
        move.Value=new Vector3(joystick.Direction.x,0,joystick.Direction.y);
    }
}
