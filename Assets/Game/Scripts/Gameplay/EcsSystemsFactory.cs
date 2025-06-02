using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "EcsSystems",
        menuName = "SampleGame/New EcsSystems"
    )]
    public sealed class EcsSystemsFactory : ScriptableObject
    {        
        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        [SerializeField]
        private EcsPrototypeCatalog _prefabCatalog;

        public IEcsSystems Create()
        {
            EcsWorld world = new EcsWorld();
            world.AddSingleton(new GameCycleData(2));
            
            EcsSystems systems = new EcsSystems(world);
            systems.AddWorld(new EcsWorld(), EcsConsts.EventWorld);

            systems
                //Game Cycle:
                .Add(new GameOverSystem())

                //Game Logic
                .Add(new TargetSystem())
                .Add(new MoveSystem())
                .Add(new RotationSystem())
                .Add(new BaseSpawnSystem(_prefabCatalog))
                .Add(new UnitSpawnSystem())
                .Add(new BulletSpawnSystem())
                // .Add(new BulletCollisionSystem())
                .Add(new LifetimeSystem())
                .Add(new DeathSystem())
                .Add(new DestroySystem())
                
                // .Add(new CharacterMoveSystem())
                // .Add(new CharacterRotateSystem())
                //.Add(new CharacterFireSystem(_bulletPrefab))

                //Rendering:
                .Add(new TransformViewSystem())
                .Add(new TeamViewSystem(_teamViewConfig))
                .Add(new FireAnimSystem())
                .Add(new TakeDamageAnimSystem())
                .Add(new MoveAnimSystem())

                //Clear:
                .ClearEvents<FireEvent>()
                .ClearEvents<TakeDamageEvent>()
                // .Add(new ClearEventSystem<FireEvent>(world))
                
                //Debug:
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsConsts.EventWorld));
#endif
            return systems;
        }
    }
}