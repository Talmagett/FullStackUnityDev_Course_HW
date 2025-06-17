using System;
using Atomic.Contexts;
using Atomic.Elements;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class CameraInstaller : IContextInstaller<IGameContext>
    {
        [SerializeField]
        private Camera _camera;

        [field: SerializeField]
        private Vector3 _cameraOffset = new(0, 7, -10);

        public void Install(IGameContext context)
        {
            context.AddCamera(_camera);
            context.AddCameraOffset(new Const<Vector3>(_cameraOffset));
            // context.AddController(new CameraFollowController());
        }
    }
}