namespace SampleGame
{
    public sealed class GameCycleData
    {
        public bool isGameOver;
        public int maxBases;
        public GameCycleData(int i)
        {
            maxBases = i;
        }
    }
}