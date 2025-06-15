using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class GameSaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly IEnumerable<IGameSerializer> _serializers;

        public GameSaveLoader(IGameRepository repository, IEnumerable<IGameSerializer> serializers)
        {
            _repository = repository;
            _serializers = serializers;
        }

        public async UniTaskVoid Save()
        {
            var gameState = new Dictionary<string, string>();
            foreach (IGameSerializer serializer in _serializers)
                serializer.Serialize(gameState);

            await _repository.SetState(gameState);
        }
        public async UniTaskVoid Load(int version = -1)
        {
            Dictionary<string, string> gameState = version == -1
                ? await _repository.GetLastState()
                : await _repository.GetVersionedState(version);

            foreach (var serializer in _serializers)
                    serializer.Deserialize(gameState);
        }

        // public async UniTaskVoid Load()
        // {
        //     Dictionary<string, string> gameState = await _repository.GetState();
        //     Debug.Log("Loaded");

        //     foreach (IGameSerializer serializer in _serializers)
        //         serializer.Deserialize(gameState);
        // }
    }
}