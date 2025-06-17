#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Contexts
{
    public sealed class ContextAPISettings : ScriptableObject
    {
        public static ContextAPISettings Instance => GetOrCreate();
        
        private static ContextAPISettings _instance;

        [SerializeField]
        public bool autoRefresh = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(autoRefresh))]
#endif
        [SerializeField]
        public float autoRefreshPeriod = 1.0f;

        private static ContextAPISettings GetOrCreate()
        {
            if (_instance != null)
                return _instance;

            string[] guids = AssetDatabase.FindAssets("t:" + nameof(ContextAPISettings));
            if (guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                ContextAPISettings settings = AssetDatabase.LoadAssetAtPath<ContextAPISettings>(assetPath);
                _instance = settings;
            }
            else
            {
                _instance = Create();
            }

            return _instance;
        }

        private static ContextAPISettings Create()
        {
            const string path = "Assets/Plugins/Atomic/Context/Editor/API/ContextAPISettings.asset";
            ContextAPISettings settings = CreateInstance<ContextAPISettings>();

            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
    }
}
#endif