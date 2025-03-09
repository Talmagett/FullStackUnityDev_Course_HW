using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private PushComponent _pushComponent;
        private TossComponent _tossComponent;

        private void Awake()
        {
            _pushComponent = character.GetComponent<PushComponent>();
            _tossComponent = character.GetComponent<TossComponent>();
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
                _tossComponent.Toss();
            }
        }
    }
}