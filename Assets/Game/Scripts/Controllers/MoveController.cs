using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private MoveComponent _moveComponent;
        private RotateComponent _rotateComponent;

        private void Awake()
        {
            _moveComponent = character.GetComponent<MoveComponent>();
            _rotateComponent = character.GetComponent<RotateComponent>();
        }

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            var horInput = Input.GetAxisRaw("Horizontal");
            
            Move(new Vector3(horInput,0));
        }

        private void Move(Vector3 direction)
        {
            _moveComponent.SetDirection(direction);
            _rotateComponent.SetDirection(direction);
        }
    }
}