#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Contexts
{
    [InitializeOnLoad]
    internal static class ContextAPIController
    {
        private static readonly ContextAPISettings _settings;
        private static double _currentTime;

        static ContextAPIController()
        {
            _settings = ContextAPISettings.Instance;
            _currentTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (!_settings.autoRefresh)
                return;

            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _currentTime >= _settings.autoRefreshPeriod)
            {
                ContextAPIManager.RefreshAPI();
                _currentTime = currentTime;
            }
        }
    }
}
#endif