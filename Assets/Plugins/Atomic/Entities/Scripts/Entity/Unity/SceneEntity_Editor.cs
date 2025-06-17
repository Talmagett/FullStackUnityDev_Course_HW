#if UNITY_EDITOR
using System;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        private void OnValidate()
        {
            this.AutoRefresh();
        }

        private void AutoRefresh()
        {
            if (!this.installInEditMode)
                return;

            if (EditorApplication.isPlaying || EditorApplication.isCompiling)
                return;

            try
            {
                this.SetRefreshCallbackToInstallers();
                this.RefreshInEditMode();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SetRefreshCallbackToInstallers()
        {
            foreach (SceneEntityInstaller installer in this.installers)
                if (installer != null)
                    installer.m_refreshCallback = this.RefreshInEditMode;
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [PropertyOrder(95)]
        [Button("Test Install"), HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 8)]
#endif
        private void RefreshInEditMode()
        {
            bool isPrefab = PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == this.gameObject;
            if (!isPrefab)
            {
                this.DisableInEditMode();
                this.DisposeInEditMode();
            }

            _entity = new Entity(this.name, _tagCapacity, _valueCapacity, _behaviourCapacity, this);
            this.InstallInternal();
            this.Precompile();

            if (!isPrefab)
            {
                _entity.Name = this.name;
                this.InitInEditMode();
                this.EnableInEditMode();
            }
        }

        private void InitInEditMode()
        {
            if (_entity.Initialized)
                return;

            foreach (IEntityBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IEntityInit dispose && IsEditModeSupported(behaviour))
                    dispose.Init(_entity);
        }

        private void EnableInEditMode()
        {
            if (_entity.Enabled)
                return;

            foreach (IEntityBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IEntityEnable dispose && IsEditModeSupported(behaviour))
                    dispose.Enable(_entity);
        }

        private void DisableInEditMode()
        {
            if (_entity is not {Enabled: true})
                return;

            foreach (IEntityBehaviour behaviour in _entity.GetBehaviours())
                if (behaviour is IEntityDisable disable && IsEditModeSupported(behaviour))
                    disable.Disable(_entity);
        }

        private void DisposeInEditMode()
        {
            if (_entity is not {Initialized: true})
                return;

            foreach (IEntityBehaviour behaviour in _entity.GetBehaviours())
            {
                if (behaviour is IEntityDispose dispose && IsEditModeSupported(behaviour))
                    dispose.Dispose(_entity);
            }
        }

        private static bool IsEditModeSupported(IEntityBehaviour behaviour)
        {
            return behaviour.GetType().IsDefined(typeof(EditModeBehaviourAttribute));
        }
    }
}

#endif