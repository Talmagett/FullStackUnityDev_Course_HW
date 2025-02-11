using Game.Scripts.System.App.Map;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "SaveLoadInstaller",
        menuName = "Zenject/App/New SaveLoadInstaller"
    )]
    public sealed class SaveLoadInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private float _savePeriod = 5;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSaveLoader>().AsSingle().OnInstantiated<GameSaveLoader>((ctx,t)=>t.Load());
            Container.BindInterfacesTo<GameSaveController>().AsSingle().WithArguments(_savePeriod);

            Container.BindInterfacesTo<MapSerializer>().AsSingle();
        }
    }
}