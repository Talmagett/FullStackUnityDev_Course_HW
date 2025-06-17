using Atomic.Entities;

namespace Game.Gameplay
{
    public static class TeamUseCase
    {
        public static bool IsEnemies(IEntity first, IEntity second)
        {
            return first.GetTeam().Value != second.GetTeam().Value;
        }
        
        public static bool IsEnemy(TeamType team, IEntity other)
        {
            return team != other.GetTeam().Value;
        }
    }
}