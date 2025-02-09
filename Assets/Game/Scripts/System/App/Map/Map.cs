using Game.App;

namespace Game.Scripts.System.App.Map
{
    public class Map
    {
        public LevelConfig CurrentLevel { get; private set; }

        public void SetLevel(LevelConfig levelConfig)
        {
            CurrentLevel = levelConfig;
        }
    }
}