using System.Collections.Generic;
using Atomic.Contexts;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using SampleGame;
using UnityEditor.SearchService;
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
        [SerializeField]
        private SceneEntity playerCharacterEntity;

        protected override void Install(IGameContext context)
        {
            context.AddWorldTransform(worldTransform);
            context.AddEntityPool(new GenericSceneEntityPool(poolTransform));
            
            bulletInstaller.Install(context);
            context.AddPlayerCharacter(playerCharacterEntity);
            context.AddKillScore(new Atomic.Elements.ReactiveInt(0));
        }
    }
}