using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "MainInstaller",
        menuName = "Zenject/App/New MainInstaller"
    )]
    public sealed class MainInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .Bind<ApplicationEvents>()
                .FromNewComponentOnRoot()
                .AsSingle();
        }
    }
}