using System.Linq;
using Game.Components;
using UnityEngine;

namespace Game.Controllers
{
    public class PushController : MonoBehaviour
    {
        [SerializeField] private GameObject character;

        private PushDetector _pushDetector;
        private PushComponent _pushComponent;
        private PushComponent _tossComponent;
        private Rigidbody2D _selfRigidbody;
        private void Awake()
        {
            _pushDetector = character.GetComponent<PushDetector>();
            _selfRigidbody = character.GetComponent<Rigidbody2D>();
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
                var targets = _pushDetector.GetPushingTargets();
                if(!targets.Any())
                    _pushComponent.Push(null,Vector3.zero);

                foreach (var pushingTarget in targets)
                {
                    if (_selfRigidbody == pushingTarget) continue;
                    _pushComponent.Push(pushingTarget,character.transform.right);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                var targets = _pushDetector.GetPushingTargets();
                if(!targets.Any())
                    _tossComponent.Push(null,Vector3.zero);

                foreach (var pushingTarget in targets)
                {
                    if (_selfRigidbody == pushingTarget) continue;
                    _tossComponent.Push(pushingTarget,character.transform.up);
                }
            }
        }
    }
}