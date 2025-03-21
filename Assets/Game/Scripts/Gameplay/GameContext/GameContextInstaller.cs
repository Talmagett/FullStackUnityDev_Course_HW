using System.Collections.Generic;
using Atomic.Contexts;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using SampleGame;
using UnityEngine;

namespace Game.Scripts
{
    public class GameContextInstaller : SceneContextInstaller<IGameContext>
    {
        [SerializeField]
        private BulletSystemInstaller bulletInstaller;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform poolTransform;

        protected override void Install(IGameContext context)
        {
            context.AddWorldTransform(worldTransform);
            context.AddEntityPool(new GenericSceneEntityPool(poolTransform));
            
            bulletInstaller.Install(context);
        }
    }
}