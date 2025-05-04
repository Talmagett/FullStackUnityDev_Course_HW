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
        var rotate = playerEntity.GetAngularDirection();
        var direction=new Vector3(joystick.Direction.x,0,joystick.Direction.y);
        move.Value=direction;
        rotate.Value=direction;
    }
}
