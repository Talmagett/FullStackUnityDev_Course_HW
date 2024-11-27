using Game.Scripts.Player;
using Modules;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Zenject
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Snake snake;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snake).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerController>().AsSingle().NonLazy();
        }
    }
}