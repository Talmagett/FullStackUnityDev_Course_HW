using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class BulletVisualInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        
        public override void Install(IEntity entity)
        {
            entity.AddMeshRenderer(_meshRenderer);
            entity.AddBehaviour<TeamColorBehaviour>();
        }
    }
}