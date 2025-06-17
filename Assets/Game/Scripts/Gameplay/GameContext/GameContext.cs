using Atomic.Contexts;

namespace Game.Gameplay
{
    public interface IGameContext : IContext
    {
    }

    public sealed class GameContext : SingletonSceneContext<GameContext>, IGameContext
    {
    }
}