using Atomic.Contexts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay
{
    public sealed class GameContextInstaller : SceneContextInstaller<IGameContext>
    {
        [SerializeField]
        private CharacterSystemInstaller _characterInstaller;
        
        [SerializeField]
        private CameraInstaller _cameraInstaller;

        [SerializeField]
        private InputMap _inputMap;

        [FormerlySerializedAs("_bulletSystemInstaller")]
        [SerializeField]
        private BulletSystemInstaller _bulletInstaller;

        [SerializeField]
        private Transform _worldTransform;

        [SerializeField]
        private TeamConfig _teamConfig;

        protected override void Install(IGameContext context)
        {
            context.AddWorldTransform(_worldTransform);
            context.AddTeamConfig(_teamConfig);
            context.AddInputMap(_inputMap);

            _characterInstaller.Install(context);
            _cameraInstaller.Install(context);
            _bulletInstaller.Install(context);
        }
    }
}