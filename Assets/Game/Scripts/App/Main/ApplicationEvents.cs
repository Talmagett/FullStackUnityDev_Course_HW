using System;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class ApplicationEvents : MonoBehaviour
    {
        public event Action OnPaused;
        public event Action OnResumed;
        public event Action OnQuit;

#if UNITY_EDITOR
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                this.OnResumed?.Invoke();
            else
                this.OnPaused?.Invoke();
        }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                this.OnPaused?.Invoke();
            else
                this.OnResumed?.Invoke();
        }
#endif

        private void OnApplicationQuit() =>
            this.OnQuit?.Invoke();
    }
}