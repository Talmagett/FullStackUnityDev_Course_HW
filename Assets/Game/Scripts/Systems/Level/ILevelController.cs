using System;

namespace Game.Scripts.Systems.Level
{
    public interface ILevelController
    {
        event Action OnLevelFinished;
    }
}