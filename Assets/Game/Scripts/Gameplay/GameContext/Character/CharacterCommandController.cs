using Atomic.Contexts;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CharacterCommandController : IContextInit<IGameContext>, IContextUpdate
    {
        private Camera _camera;
        private InputMap _inputMap;
        private IEntity _character;

        public void Init(IGameContext context)
        {
            _camera = context.GetCamera();
            _inputMap = context.GetInputMap();
            _character = context.GetCharacter();
        }

        public void OnUpdate(IContext context, float deltaTime)
        {
            bool additive = Input.GetKey(_inputMap.Additive);

            if (Input.GetKeyDown(_inputMap.Stop))
            {
                // CommandUseCase.Stop(_character);
                return;
            }

            if (Input.GetKeyDown(_inputMap.Hold))
            {
                // CommandUseCase.Hold(_character, additive);
                return;
            }

            if (!Input.GetMouseButtonDown(_inputMap.Click))
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit raycastHit))
                return;
            
            if (Input.GetKey(_inputMap.Move))
            {
                // CommandUseCase.Move(_character, raycastHit, additive);
            }
            else if (Input.GetKey(_inputMap.Patrol))
            {
                // CommandUseCase.Patrol(_character, raycastHit, additive);
            }
            else if (Input.GetKey(_inputMap.Attack))
            {
                // CommandUseCase.Attack(_character, raycastHit, additive);
            }
            else if (Input.GetKey(_inputMap.Follow))
            {
                // CommandUseCase.Follow(_character, raycastHit, additive);
            }
        }
    }
}