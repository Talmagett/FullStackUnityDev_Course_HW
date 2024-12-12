using Modules;

namespace SnakeGame.Systems
{
    public class GameStarter
    {
        private readonly IDifficulty _difficulty;

        public GameStarter(IDifficulty difficulty)
        {
            _difficulty = difficulty;
        }
        
        public void StartGame()
        {
            if (_difficulty.Current == 0)
                _difficulty.Next(out var d);
        }
    }
}