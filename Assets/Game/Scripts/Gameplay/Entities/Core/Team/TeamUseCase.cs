using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public readonly struct TeamUseCase
    {
        private readonly EcsPoolInject<TeamType> _teamType;
        
        public bool IsEnemies(in int source, in int target)
        {
            return _teamType.Value.Get(source) != _teamType.Value.Get(target);
        }
        
        public bool IsTeam(in int source, TeamType teamType)
        {
            return _teamType.Value.Get(source) == teamType;
        }
    }
}