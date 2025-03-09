using UnityEngine;

namespace SampleGame
{
    public class ReloadComponent : MonoBehaviour
    {
        [SerializeField] private float _maxTime;
        private float _currentTime;
        private bool _isReady;

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _maxTime && !_isReady)
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