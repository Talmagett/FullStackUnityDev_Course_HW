using System.Collections.Generic;

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

        public void Save()
        {
            var gameState = new Dictionary<string, string>();
            foreach (IGameSerializer serializer in _serializers)
                serializer.Serialize(gameState);

            _repository.SetState(gameState);
        }

        public void Load()
        {
            Dictionary<string, string> gameState = _repository.GetState();
            foreach (IGameSerializer serializer in _serializers)
                serializer.Deserialize(gameState);
        }
    }
}