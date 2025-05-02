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
        private InputMap _inputMap;
        
        [SerializeField]
        private TeamViewConfig _teamViewConfig;

        [SerializeField]
        private EcsPrototype _bulletPrefab;

        public IEcsSystems Create()
        {
            EcsWorld world = new EcsWorld();
            world.AddSingleton(new InputData());
            
            EcsSystems systems = new EcsSystems(world);
            systems.AddWorld(new EcsWorld(), EcsConsts.EventWorld);

            systems

                //Input:
                .Add(new InputSystem(_inputMap))
                .Add(new PlayerMoveController())
                .Add(new PlayerFireController())

                //Game Logic
                .Add(new MoveSystem())
                .Add(new RotationSystem())
                .Add(new BulletSpawnSystem())
                .Add(new BulletCollisionSystem())
                .Add(new LifetimeSystem())
                .Add(new DeathSystem())
                .Add(new DestroySystem())
                
                .Add(new CharacterMoveSystem())
                .Add(new CharacterRotateSystem())
                .Add(new CharacterFireSystem(_bulletPrefab))

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