using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private IPushComponent _pushComponent;
        private ITossComponent _tossComponent;
        private void Awake()
        {
            _pushComponent = character.GetComponent<IPushComponent>();
            _tossComponent = character.GetComponent<ITossComponent>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _pushComponent.Push();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _tossComponent.Toss();
            }
        }
    }
}