using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private MoveComponent _horizontalMoveComponent;

        private void Awake()
        {
            _horizontalMoveComponent = character.GetComponent<MoveComponent>();
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
            _horizontalMoveComponent.SetDirection(direction);
        }
    }
}