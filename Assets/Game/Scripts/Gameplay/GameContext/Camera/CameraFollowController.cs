using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class CameraFollowController : IContextInit<IGameContext>, IContextLateUpdate
    {
        private IEntity _character;
        private Transform _camera;
        private IValue<Vector3> _offset;

        public void Init(IGameContext context)
        {
            _character = context.GetCharacter();
            _camera = context.GetCamera().transform;
            _offset = context.GetCameraOffset();
        }

        public void OnLateUpdate(IContext context, float deltaTime)
        {
            _camera.position = _character.GetTransform().position + _offset.Value;
        }
    }
}