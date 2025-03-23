using UnityEngine;

namespace Game.Common
{
    [System.Serializable]
    public class Cooldown
    {
        [SerializeField] private float maxTime;
        private float _currentTime;
        private bool _isReady;

        public void TickUpdate(float deltaTime)
        {
            if (IsReady())
                return;
            
            _currentTime += deltaTime;

            if (_currentTime > maxTime && !_isReady)
            {
                _isReady = true;
            }
        }

        public bool IsReady()
        {
            return _isReady;
        }

        public void Reload()
        {
            _isReady = false;
            _currentTime = 0f;
        }
    }
}