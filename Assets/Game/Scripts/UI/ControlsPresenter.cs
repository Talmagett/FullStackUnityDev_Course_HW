using System;
using SampleGame.App;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private GameSaveLoader _gameSaveLoader;
        public ControlsPresenter(GameSaveLoader gameSaveLoader)
        {
            _gameSaveLoader = gameSaveLoader ?? throw new ArgumentNullException(nameof(gameSaveLoader));
        }
        public void Save(Action<bool, int> callback)
        {
            if (_gameSaveLoader == null)
            {
                callback.Invoke(false, -1);
                return;
            }
            _gameSaveLoader.Save().Forget();
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            if (_gameSaveLoader == null)
            {
                callback.Invoke(false, -1);
                return;
            }
            _gameSaveLoader.Load().Forget();
        }
    }
}