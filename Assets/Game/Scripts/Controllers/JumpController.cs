using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private JumpComponent jumpComponent;
        private RotateComponent _rotateComponent;

        private void Awake()
        {
            jumpComponent = character.GetComponent<JumpComponent>();
        }

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                jumpComponent.Jump();
        }
    }
}