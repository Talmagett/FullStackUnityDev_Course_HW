using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private PushComponent _pushComponent;
        private PushComponent _tossComponent;

        private void Awake()
        {
            var pushComponents = character.GetComponents<PushComponent>();
            _pushComponent = pushComponents[0];
            _tossComponent = pushComponents[1];
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pushComponent.Push(character.transform.right);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _tossComponent.Push(character.transform.up);
            }
        }
    }
}