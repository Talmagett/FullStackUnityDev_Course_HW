using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "RepositoryInstaller",
        menuName = "Zenject/App/New RepositoryInstaller"
    )]
    public sealed class RepositoryInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private string _fileName = "GameState.txt";

        [SerializeField]
        private string _aesPassword = "123";

        [SerializeField]
        private byte[] _aesSalt = {0x52, 0x41, 0x16, 0x79, 0x86, 0x64, 0x97, 0x22};
        
        public override void InstallBindings()
        {
            string filePath = $"{Application.streamingAssetsPath}/{_fileName}";
            this.Container.BindInterfacesTo<GameRepository>().AsSingle().WithArguments(filePath, _aesPassword, _aesSalt);
        }
    }
}